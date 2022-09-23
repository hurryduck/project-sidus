using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StarBlockArray
{
    public Transform ArrayPosition;
    public StarBlock starBlock;
}

[System.Serializable]
public class BombBlockArray
{
    public Transform ArrayPosition;
    public BombBlock bombBlock;
}

public class StarBoard : MonoBehaviour
{
    private static StarBoard instance;
    public static StarBoard Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<StarBoard>();

            return instance;
        }
    }

    [SerializeField] private StarBlockArray[] StarTiles;
    public BombBlockArray[] BombTiles;

    private float CurrentTime = 0f;

    [HideInInspector] public int DontMoveStarBlockNum;        // �������� �ʴ� ���� ���� ����
    [HideInInspector] public int CancerStarBlockNum;          // ������ �ϳ��� ���� ���� ����
    [HideInInspector] public int DoubleBombStarBlockNum;      // �ι� ������ ���� ���� ����

    private int DestoryStarBlockTilesTime;  // ���� ��� ������� �ϴ� �ð�
    private int DestoryStarBlockTilesNum;   // ����� �� ����

    private void Start()
    {
        // Ȳ���ڸ� ���� �� ������
        if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Taurus))
            for (int i = 0; i < 4; i++)
            {
                int randomNum = Random.Range(0, StarTiles.Length);
                StarTiles = StarTiles.Where((source, index) => index != randomNum).ToArray();
            }

        // ������ڸ� �� ������� �ϴ� �ð� ����
        if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Pisces))
            DestoryStarBlockTilesTime = Random.Range(1, 6);

        // ���� ��ġ
        for (int i = 0; i < StarTiles.Length; i++)
            StartCoroutine(PlaceStarBlcok(StarTiles[i].ArrayPosition, i));

        for (int i = 0; i < 4; i++) PlaceBombBlock(BombTiles[i].ArrayPosition, i, 2);    // Top
        for (int i = 4; i < 8; i++) PlaceBombBlock(BombTiles[i].ArrayPosition, i, 3);    // Right
        for (int i = 8; i < 12; i++) PlaceBombBlock(BombTiles[i].ArrayPosition, i, 0);   // Bottom
        for (int i = 12; i < 16; i++) PlaceBombBlock(BombTiles[i].ArrayPosition, i, 1);  // Left
    }

    private void Update()
    {
        switch (GameManager.Instance.CurrentChapter)
        {
            case GameManager.ChapterType.Libra:          // õĪ�ڸ� ���� ������ �ʰ� �ϱ�
                CurrentTime += Time.deltaTime;
                if (CurrentTime > 5)
                {
                    Instantiate(Resources.Load("Prefabs/BlindImage"), GameObject.Find("CanvasCenter").transform);
                    CurrentTime = 0;
                }
                break;

            case GameManager.ChapterType.Pisces:         // ����� �ڸ� �� ������� �ϱ�
                CurrentTime += Time.deltaTime;
                if (CurrentTime > DestoryStarBlockTilesTime)
                {
                    if (DestoryStarBlockTilesNum < 5)
                        StartCoroutine(DestroyStarBlockTiles());

                    CurrentTime = 0;
                    DestoryStarBlockTilesTime = Random.Range(0, 6);
                }
                break;
        }
    }

    public IEnumerator PlaceStarBlcok(Transform transform, int ArrayNum, float waittime = 0f)
    {
        yield return new WaitForSeconds(waittime);      // starblock�� �μ����� ���ο� ���� ������ ���� �������� �ð���ŭ ��ٷȴٰ� ����
        switch (GameManager.Instance.CurrentChapter)
        {
            // ���ڸ� �������� �ʴ� ��
            case GameManager.ChapterType.Aries:
                if (ArrayNum == 2 || ArrayNum == 4 || ArrayNum == 11 || ArrayNum == 13)
                    Star_DontMove(transform, ArrayNum);
                else
                    Star_Standard(transform, ArrayNum);
                break;

            // ó���ڸ� �ι� Ŭ�� ��
            case GameManager.ChapterType.Virgo:
                if (Random.value < 0.5f)
                    Star_Standard(transform, ArrayNum);
                else
                    Star_DoubleClick(transform, ArrayNum);
                break;

            // �����ڸ� �������� �ʴ� ��
            case GameManager.ChapterType.Scorpio:
                if (Random.value < 0.9f)
                    Star_Standard(transform, ArrayNum);
                else
                {
                    if (DontMoveStarBlockNum < 6)
                    {
                        Star_DontMove(transform, ArrayNum);
                        DontMoveStarBlockNum++;
                    }
                    else
                        Star_Standard(transform, ArrayNum);
                }
                break;

            // �����ڸ� �ι� ������ ��
            case GameManager.ChapterType.Aquarius:
                if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Aquarius))
                {
                    if (Random.value < 0.9f)
                        Star_Standard(transform, ArrayNum);
                    else
                    {
                        if (DoubleBombStarBlockNum < 6)
                        {
                            Star_DoubleBomb(transform, ArrayNum);
                            DoubleBombStarBlockNum++;
                        }
                        else
                            Star_Standard(transform, ArrayNum);
                    }
                }
                break;

            default:
                Star_Standard(transform, ArrayNum);
                break;
        }
    }

    private void Star_Standard(Transform transform, int ArrayNum)
    {
        string randomNum = Random.Range(1, 4).ToString();
        InstantiateStarBlock("P_Star_Branch" + randomNum, transform, ArrayNum);
    }

    private void Star_DontMove(Transform transform, int ArrayNum)
    {
        string randomNum = Random.Range(1, 4).ToString();
        InstantiateStarBlock("P_Star_DM_Branch" + randomNum, transform, ArrayNum);
    }

    private void Star_DoubleBomb(Transform transform, int ArrayNum)
    {
        string randomNum = Random.Range(1, 4).ToString();
        InstantiateStarBlock("P_Star_DB_Branch" + randomNum, transform, ArrayNum);
    }

    private void Star_DoubleClick(Transform transform, int ArrayNum)
    {
        string randomNum = Random.Range(1, 4).ToString();
        InstantiateStarBlock("P_Star_DC_Branch" + randomNum, transform, ArrayNum);
    }

    public void InstantiateStarBlock(string StarBlockPrefabName, Transform transform, int ArrayNum)
    {
        string prefabName = "Prefabs/StarBlocks/" + StarBlockPrefabName;
        StarTiles[ArrayNum].starBlock = (Instantiate(Resources.Load(prefabName), transform.position, Quaternion.identity) as GameObject).GetComponent<StarBlock>();
        StarTiles[ArrayNum].starBlock.MyPosition = ArrayNum;
    }

    private IEnumerator DestroyStarBlockTiles()
    {
        int randomNum;
        do
        {
            randomNum = Random.Range(0, StarTiles.Length);
        }
        while (StarTiles[randomNum].starBlock == null);

        Destroy(StarTiles[randomNum].starBlock.gameObject);
        StartCoroutine(PlaceStarBlcok(StarTiles[randomNum].ArrayPosition, randomNum, 6));

        yield return new WaitForSeconds(6f);
        DestoryStarBlockTilesNum--;
    }

    private void PlaceBombBlock(Transform transform, int arrayNum, int haveToTrue)
    {
        string randomNum = Random.Range(1, 4).ToString();
        BombBlock bombBlock = Instantiate(
            Resources.Load<GameObject>("Prefabs/BombBlocks/P_BB_Branch" + randomNum),
            transform.position,
            Quaternion.identity).GetComponent<BombBlock>();

        BombTiles[arrayNum].bombBlock = bombBlock;
        bombBlock.HaveToTrue = haveToTrue;
    }

    public IEnumerator PlaceBombBlock(Transform transform, int arrayNum, int haveToTrue, int time)
    {
        yield return new WaitForSeconds(time);
        string randomNum = Random.Range(1, 4).ToString();
        BombBlock bombBlock = Instantiate(
            Resources.Load<GameObject>("Prefabs/BombBlocks/P_BB_Branch" + randomNum),
            transform.position,
            Quaternion.identity).GetComponent<BombBlock>();

        BombTiles[arrayNum].bombBlock = bombBlock;
        bombBlock.HaveToTrue = haveToTrue;
    }
}

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
public class ShootingStarArray
{
    public Transform ArrayPosition;
    public Image SelectImage;
    public bool IsClicked;
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

    [SerializeField]
    private StarBlockArray[] starBlcokArray;

    [SerializeField]
    private ShootingStarArray[] shootingStarArray;
    private float Shottime = 0f;

    private float CurrentTime = 0f;

    [HideInInspector] public int DontMoveStarBlockNum;        // �������� �ʴ� ���� ���� ����
    [HideInInspector] public int CancerStarBlockNum;          // ������ �ϳ��� ���� ���� ����
    [HideInInspector] public int DoubleBombStarBlockNum;      // �ι� ������ ���� ���� ����

    private int DestoryStarBlockTilesTime;  // ���� ��� ������� �ϴ� �ð�
    private int DestoryStarBlockTilesNum;   // ����� �� ����

    private void Start()
    {
        if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Taurus))        // Ȳ���ڸ� ���� �� ������
            for (int i = 0; i < 4; i++)
            {
                int randomNum = Random.Range(0, starBlcokArray.Length);
                starBlcokArray = starBlcokArray.Where((source, index) => index != randomNum).ToArray();
            }

        if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Pisces))      // ������ڸ� ��� ������� �ϴ� �ð� ����
            DestoryStarBlockTilesTime = Random.Range(1, 6);

        for (int i = 0; i < starBlcokArray.Length; i++)                                 // starblock ��ġ �ϱ�
            StartCoroutine(PlacedStarBlcok(starBlcokArray[i].ArrayPosition, i));
    }

    private void Update()
    {
        Shottime += Time.deltaTime;                     // ���˺� �������� �ð�

        switch (GameManager.Instance.CurrentChapter)
        {
            case GameManager.ChapterType.Cancer:         // ���ڸ� �ð����� (0, 0) ��ġ�� ����� Cancer�� ����
                CurrentTime += Time.deltaTime;
                if (CurrentTime > 9)
                {
                    if (!starBlcokArray[0].starBlock.starType.Equals(StarBlock.StarType.Cancer))
                    {
                        Destroy(starBlcokArray[0].starBlock.gameObject);
                        InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_Cancer_Branch1", starBlcokArray[0].ArrayPosition, 0);
                    }
                    CurrentTime = 0f;
                }
                break;

            case GameManager.ChapterType.Leo:            // �����ڸ� ���˺� �����Ÿ�Ͽ��� �������� ����
                break;

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

    public IEnumerator PlacedStarBlcok(Transform transform, int ArrayNum, float waittime = 0f)
    {
        yield return new WaitForSeconds(waittime);      // starblock�� �μ����� ���ο� ���� ������ ���� �������� �ð���ŭ ��ٷȴٰ� ����

        if ((GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Aries)) && (ArrayNum == 6 || ArrayNum == 8 || ArrayNum == 16 || ArrayNum == 18))
        {                                                                                       // ���ڸ� ������ �ʴ� ��
            RandomStarBlockDontMove(transform, ArrayNum);
        }
        else if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Virgo))          // ó���ڸ� ����Ŭ�� ��
        {
            if (Random.value < 0.5f)
                RandomStarBlockStandard(transform, ArrayNum);
            else
                RandomStarBlockDoubleClick(transform, ArrayNum);
        }
        else if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Scorpio))        // �����ڸ� �������� �ʴ� ��
        {
            if (Random.value < 0.9f)
                RandomStarBlockStandard(transform, ArrayNum);
            else
            {
                if (DontMoveStarBlockNum < 6)
                {
                    RandomStarBlockDontMove(transform, ArrayNum);
                    DontMoveStarBlockNum++;
                }
                else
                    RandomStarBlockStandard(transform, ArrayNum);
            }
        }
        else if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Sagittarius))    // �ü��ڸ� ������ �ϳ��� ��
        {
            if (Random.value < 0.9f)
                RandomStarBlockStandard(transform, ArrayNum);
            else
            {
                if (CancerStarBlockNum < 6)
                {
                    InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_Cancer_Branch1", transform, ArrayNum);
                    CancerStarBlockNum++;
                }
                else
                    RandomStarBlockStandard(transform, ArrayNum);
            }
        }
        else if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Aquarius))       // �����ڸ� 2�� �μž� �μ����� ��
        {
            if (Random.value < 0.9f)
                RandomStarBlockStandard(transform, ArrayNum);
            else
            {
                if (DoubleBombStarBlockNum < 6)
                {
                    RandomStarBlockDoubleBomb(transform, ArrayNum);
                    DoubleBombStarBlockNum++;
                }
                else
                    RandomStarBlockStandard(transform, ArrayNum);
            }
        }
        else
            RandomStarBlockStandard(transform, ArrayNum);       // ���� �⺻�� �����ϰ� �����ϱ�
    }

    private void RandomStarBlockStandard(Transform transform, int ArrayNum)
    {
        int randomNum = Random.Range(1, 4);

        switch (randomNum)
        {
            case 1: // �⺻ ����2����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_Branch2", transform, ArrayNum);
                break;
            case 2: // �⺻ ���� 2���� ����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_Branch2Straight", transform, ArrayNum);
                break;
            case 3: // �⺻ ����3����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_Branch3", transform, ArrayNum);
                break;
        }
    }

    private void RandomStarBlockDontMove(Transform transform, int ArrayNum)
    {
        int randomNum = Random.Range(1, 4);

        switch (randomNum)
        {
            case 1: // �������� �ʴ� ����2����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DontMove_Branch2", transform, ArrayNum);
                break;
            case 2: // �������� �ʴ� ���� 2���� ����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DontMove_Branch2Straight", transform, ArrayNum);
                break;
            case 3: // �������� �ʴ� ����3����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DontMove_Branch3", transform, ArrayNum);
                break;
        }
    }

    private void RandomStarBlockDoubleBomb(Transform transform, int ArrayNum)
    {
        int randomNum = Random.Range(1, 4);

        switch (randomNum)
        {
            case 1: // �ι� �Ͷ߷��� �ϴ� ����2����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DoubleBomb_Branch2", transform, ArrayNum);
                break;
            case 2: // �ι� �Ͷ߷��� �ϴ� ���� 2���� ����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DoubleBomb_Branch2Straight", transform, ArrayNum);
                break;
            case 3: // �ι� �Ͷ߷��� �ϴ� ����3����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DoubleBomb_Branch3", transform, ArrayNum);
                break;
        }
    }

    private void RandomStarBlockDoubleClick(Transform transform, int ArrayNum)
    {
        int randomNum = Random.Range(1, 4);

        switch (randomNum)
        {
            case 1: // �ι� ������ ȸ�� ����2����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DoubleClick_Branch2", transform, ArrayNum);
                break;
            case 2: // �ι� ������ ȸ�� ���� 2���� ����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DoubleClick_Branch2Straight", transform, ArrayNum);
                break;
            case 3: // �ι� ������ ȸ�� ����3����
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DoubleClick_Branch3", transform, ArrayNum);
                break;
        }
    }

    public void InstantiateStarBlock(string StarBlockPrefabName, Transform transform, int ArrayNum)
    {
        starBlcokArray[ArrayNum].starBlock = (Instantiate(Resources.Load(StarBlockPrefabName), transform.position, Quaternion.identity) as GameObject).GetComponent<StarBlock>();
        starBlcokArray[ArrayNum].starBlock.MyPosition = ArrayNum;
    }

    private IEnumerator DestroyStarBlockTiles()
    {
        int randomNum;
        do
        {
            randomNum = Random.Range(0, starBlcokArray.Length);
        }
        while (starBlcokArray[randomNum].starBlock == null);

        Destroy(starBlcokArray[randomNum].starBlock.gameObject);
        StartCoroutine(PlacedStarBlcok(starBlcokArray[randomNum].ArrayPosition, randomNum, 6));

        yield return new WaitForSeconds(6f);
        DestoryStarBlockTilesNum--;
    }

    public void _ShootingClick(int Index)
    {
        if (Shottime > 0)
        {
            if (shootingStarArray[Index].IsClicked)
            {
                shootingStarArray[Index].IsClicked = false;
                ChangeAlpah(Index, 0);
                Shottime = 0;
                Instantiate(Resources.Load("Prefabs/StarBlocks/ShootingStarBlock"), shootingStarArray[Index].ArrayPosition.position, Quaternion.identity);
            }
            else
            {
                for (int i = 0; i < shootingStarArray.Length; i++)
                {
                    shootingStarArray[i].IsClicked = false;
                    ChangeAlpah(i, 0);
                }
                shootingStarArray[Index].IsClicked = true;
                ChangeAlpah(Index, 1);
            }
        }
    }

    private void ChangeAlpah(int Index, int Alpha)
    {
        Color alpha = shootingStarArray[Index].SelectImage.color;
        alpha.a = Alpha;
        shootingStarArray[Index].SelectImage.color = alpha;
    }
}

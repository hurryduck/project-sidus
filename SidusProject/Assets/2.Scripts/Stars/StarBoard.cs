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

    [HideInInspector] public int DontMoveStarBlockNum;        // 움직이지 않는 별블럭 제한 변수
    [HideInInspector] public int CancerStarBlockNum;          // 가지가 하나인 별블럭 제한 변수
    [HideInInspector] public int DoubleBombStarBlockNum;      // 두번 터지는 별블럭 제한 변수

    private int DestoryStarBlockTilesTime;  // 다음 블록 사라지게 하는 시간
    private int DestoryStarBlockTilesNum;   // 사라진 블럭 개수

    private void Start()
    {
        if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Taurus))        // 황소자리 랜덤 블럭 없어짐
            for (int i = 0; i < 4; i++)
            {
                int randomNum = Random.Range(0, starBlcokArray.Length);
                starBlcokArray = starBlcokArray.Where((source, index) => index != randomNum).ToArray();
            }

        if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Pisces))      // 물고기자리 블록 사라지게 하는 시간 설정
            DestoryStarBlockTilesTime = Random.Range(1, 6);

        for (int i = 0; i < starBlcokArray.Length; i++)                                 // starblock 배치 하기
            StartCoroutine(PlacedStarBlcok(starBlcokArray[i].ArrayPosition, i));
    }

    private void Update()
    {
        Shottime += Time.deltaTime;                     // 별똥별 떨어지는 시간

        switch (GameManager.Instance.CurrentChapter)
        {
            case GameManager.ChapterType.Cancer:         // 게자리 시간마다 (0, 0) 위치의 별블록 Cancer로 변경
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

            case GameManager.ChapterType.Leo:            // 사자자리 별똥별 별블록타일에도 떨어지게 설정
                break;

            case GameManager.ChapterType.Libra:          // 천칭자리 별블럭 보이지 않게 하기
                CurrentTime += Time.deltaTime;
                if (CurrentTime > 5)
                {
                    Instantiate(Resources.Load("Prefabs/BlindImage"), GameObject.Find("CanvasCenter").transform);
                    CurrentTime = 0;
                }
                break;

            case GameManager.ChapterType.Pisces:         // 물고기 자리 블럭 사라지게 하기
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
        yield return new WaitForSeconds(waittime);      // starblock이 부셔지고 새로운 블럭을 생성할 때에 지정해준 시간만큼 기다렸다가 실행

        if ((GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Aries)) && (ArrayNum == 6 || ArrayNum == 8 || ArrayNum == 16 || ArrayNum == 18))
        {                                                                                       // 양자리 움직이 않는 블럭
            RandomStarBlockDontMove(transform, ArrayNum);
        }
        else if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Virgo))          // 처녀자리 더블클릭 블럭
        {
            if (Random.value < 0.5f)
                RandomStarBlockStandard(transform, ArrayNum);
            else
                RandomStarBlockDoubleClick(transform, ArrayNum);
        }
        else if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Scorpio))        // 전갈자리 움직이지 않는 블럭
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
        else if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Sagittarius))    // 궁수자리 가지가 하나인 블럭
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
        else if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Aquarius))       // 물병자리 2번 부셔야 부서지는 블럭
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
            RandomStarBlockStandard(transform, ArrayNum);       // 별블럭 기본을 랜덤하게 생성하기
    }

    private void RandomStarBlockStandard(Transform transform, int ArrayNum)
    {
        int randomNum = Random.Range(1, 4);

        switch (randomNum)
        {
            case 1: // 기본 별블럭2가지
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_Branch2", transform, ArrayNum);
                break;
            case 2: // 기본 별블럭 2가지 직선
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_Branch2Straight", transform, ArrayNum);
                break;
            case 3: // 기본 별블럭3가지
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_Branch3", transform, ArrayNum);
                break;
        }
    }

    private void RandomStarBlockDontMove(Transform transform, int ArrayNum)
    {
        int randomNum = Random.Range(1, 4);

        switch (randomNum)
        {
            case 1: // 움직이지 않는 별블럭2가지
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DontMove_Branch2", transform, ArrayNum);
                break;
            case 2: // 움직이지 않는 별블럭 2가지 직선
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DontMove_Branch2Straight", transform, ArrayNum);
                break;
            case 3: // 움직이지 않는 별블럭3가지
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DontMove_Branch3", transform, ArrayNum);
                break;
        }
    }

    private void RandomStarBlockDoubleBomb(Transform transform, int ArrayNum)
    {
        int randomNum = Random.Range(1, 4);

        switch (randomNum)
        {
            case 1: // 두번 터뜨려야 하는 별블럭2가지
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DoubleBomb_Branch2", transform, ArrayNum);
                break;
            case 2: // 두번 터뜨려야 하는 별블럭 2가지 직선
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DoubleBomb_Branch2Straight", transform, ArrayNum);
                break;
            case 3: // 두번 터뜨려야 하는 별블럭3가지
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DoubleBomb_Branch3", transform, ArrayNum);
                break;
        }
    }

    private void RandomStarBlockDoubleClick(Transform transform, int ArrayNum)
    {
        int randomNum = Random.Range(1, 4);

        switch (randomNum)
        {
            case 1: // 두번 눌러야 회전 별블럭2가지
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DoubleClick_Branch2", transform, ArrayNum);
                break;
            case 2: // 두번 눌러야 회전 별블럭 2가지 직선
                InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_DoubleClick_Branch2Straight", transform, ArrayNum);
                break;
            case 3: // 두번 눌러야 회전 별블럭3가지
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

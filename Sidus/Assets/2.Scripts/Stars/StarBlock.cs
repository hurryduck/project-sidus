using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StarBlock : MonoBehaviour
{
    private enum StarShape { Branch1, Branch2, Branch2_Straight, Branch3 }
    [SerializeField]
    private StarShape starShape;

    public enum StarType { Standard, DontMove, DoubleClick, DoubleBomb, Cancer}
    public StarType starType;

    public enum StarState { Waiting, Moving, Bomb }
    [HideInInspector]
    public StarState starState;

    [SerializeField]
    private Sprite BombedStarBlockSprite;

    [HideInInspector]
    public int MyPosition;

    private bool IsBomb = false;

    private float currentrotAngle;
    private float rotAngle;

    // ���� ��ġ ����
    private enum StandardBranch { Top, Right, Bottom, Left }
    private StandardBranch standardBranch;
    private Vector3[] StarBlockBranchs;
    private Vector3 TopVector = new Vector3(0, 1, 0);
    private Vector3 RightVector = new Vector3(1, 0, 0);
    private Vector3 BottomVector = new Vector3(0, -1, 0);
    private Vector3 LeftVector = new Vector3(-1, 0, 0);

    private float CurrentTime;
    private bool IsClicked = false;
    private bool IsBombOnce = false;

    private void Awake()
    {
        starState = StarState.Waiting;

        // ���� ���� ����
        standardBranch = StandardBranch.Top;
        switch (starShape)
        {
            case StarShape.Branch1:
                StarBlockBranchs = new Vector3[1];
                StarBlockBranchs[0] = TopVector;
                break;


            case StarShape.Branch2:
                StarBlockBranchs = new Vector3[2];
                StarBlockBranchs[0] = TopVector;
                StarBlockBranchs[1] = RightVector;
                break;

            case StarShape.Branch2_Straight:
                StarBlockBranchs = new Vector3[2];
                StarBlockBranchs[0] = TopVector;
                StarBlockBranchs[1] = BottomVector;
                break;

            case StarShape.Branch3:
                StarBlockBranchs = new Vector3[3];
                StarBlockBranchs[0] = TopVector;
                StarBlockBranchs[1] = RightVector;
                StarBlockBranchs[2] = LeftVector;
                break;
        }
    }

    private void Start()
    {
        int RandomNum = Random.Range(0, 4);
        for (int i = 0; i < RandomNum; i++)
            ChangeBranch();
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, RandomNum * -90); // ȸ��
    }

    private void Update()
    {
        if (starState.Equals(StarState.Bomb) && !IsBomb)
        {
            if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Aquarius) && starType.Equals(StarType.DoubleBomb))
            {
                Debug.Log(1);
                if (IsBombOnce)
                {
                    IsBomb = true;
                    StartCoroutine(BomebStarBlock());
                }
                else
                {
                    for(int i = 0; i < transform.childCount; i++)
                        transform.GetChild(i).tag = "Untagged";
                    starState = StarState.Waiting;
                    gameObject.GetComponent<SpriteRenderer>().sprite = BombedStarBlockSprite;
                    IsBombOnce = true;
                }
            }
            else
            {
                IsBomb = true;
                StartCoroutine(BomebStarBlock());
            }
        }

        if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Cancer) && starType.Equals(StarType.Cancer))
        {
            CurrentTime += Time.deltaTime;
            if(CurrentTime > 15)
            {
                List<RaycastHit2D> Hits = new List<RaycastHit2D>();
                Hits = Physics2D.RaycastAll(transform.position, new Vector3(1, 0, 0),  1f, LayerMask.GetMask("StarBlock")).ToList();
                foreach (RaycastHit2D Hit in Hits)
                    Hit.transform.GetComponent<StarBlock>().ChangeStarType(StarType.Cancer);
                Hits.Clear();

                Hits = Physics2D.RaycastAll(transform.position, new Vector3(-1, 0, 0), 1f, LayerMask.GetMask("StarBlock")).ToList();
                foreach (RaycastHit2D Hit in Hits)
                    Hit.transform.GetComponent<StarBlock>().ChangeStarType(StarType.Cancer);
                Hits.Clear();

                Hits = Physics2D.RaycastAll(transform.position, new Vector3(0, 1, 0),  1f, LayerMask.GetMask("StarBlock")).ToList();
                foreach (RaycastHit2D Hit in Hits)
                    Hit.transform.GetComponent<StarBlock>().ChangeStarType(StarType.Cancer);
                Hits.Clear();

                Hits = Physics2D.RaycastAll(transform.position, new Vector3(0, -1, 0), 1f, LayerMask.GetMask("StarBlock")).ToList();
                foreach (RaycastHit2D Hit in Hits)
                    Hit.transform.GetComponent<StarBlock>().ChangeStarType(StarType.Cancer);
                Hits.Clear();

                CurrentTime = 0;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!InGameManager.Instance.IsPaused && starState.Equals(StarState.Waiting))
        {
            if(!starType.Equals(StarType.DontMove))
            {
                if (starType.Equals(StarType.DoubleClick))
                {
                    if (IsClicked)
                    {
                        starState = StarState.Moving;
                        StartCoroutine(rotationStarBlock());
                    }
                    IsClicked = !IsClicked;
                }
                else
                {
                    starState = StarState.Moving;
                    StartCoroutine(rotationStarBlock());
                }
            }
        }
    }

    private IEnumerator rotationStarBlock() // starblock�� ȸ����Ű�� �Լ�
    {
        currentrotAngle = transform.rotation.eulerAngles.z;         // ���� �����̼� z�� �޾ƿ�(���Ϸ� transfrom���� �޾ƿ�)
        rotAngle = currentrotAngle - 90f;           // 90�� ���ư� �����̼� ����
        while (rotAngle < currentrotAngle)          // ��ǥ �����̼ǿ� �����Ҷ����� �ݺ�
        {
            currentrotAngle -= 10f;                  // ȸ�� �ӵ� ����
            if (rotAngle > currentrotAngle)         // ���� �� ��ǥ �����̼��� �Ѿ��� �� �ߵ�
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotAngle);    // ���� �����̼����� ����
                break;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, currentrotAngle); // ȸ��
            yield return null;        // ���� �ð����� ����
        }
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotAngle);    // ��ǥ �����̼� ����

        ChangeBranch(); // ������� ��ġ ����

        starState = StarState.Waiting;
    }

    private void ChangeBranch()
    {
        switch (starShape)
        {
            case StarShape.Branch1:
                switch (standardBranch)
                {
                    case StandardBranch.Top:
                        standardBranch = StandardBranch.Right;
                        StarBlockBranchs[0] = RightVector;
                        break;

                    case StandardBranch.Right:
                        standardBranch = StandardBranch.Bottom;
                        StarBlockBranchs[0] = BottomVector;
                        break;

                    case StandardBranch.Bottom:
                        standardBranch = StandardBranch.Left;
                        StarBlockBranchs[0] = LeftVector;
                        break;

                    case StandardBranch.Left:
                        standardBranch = StandardBranch.Top;
                        StarBlockBranchs[0] = TopVector;
                        break;
                }
                break;

            case StarShape.Branch2:
                switch (standardBranch)
                {
                    case StandardBranch.Top:
                        standardBranch = StandardBranch.Right;
                        StarBlockBranchs[0] = RightVector;
                        StarBlockBranchs[1] = BottomVector;
                        break;

                    case StandardBranch.Right:
                        standardBranch = StandardBranch.Bottom;
                        StarBlockBranchs[0] = BottomVector;
                        StarBlockBranchs[1] = LeftVector;
                        break;

                    case StandardBranch.Bottom:
                        standardBranch = StandardBranch.Left;
                        StarBlockBranchs[0] = LeftVector;
                        StarBlockBranchs[1] = TopVector;
                        break;

                    case StandardBranch.Left:
                        standardBranch = StandardBranch.Top;
                        StarBlockBranchs[0] = TopVector;
                        StarBlockBranchs[1] = RightVector;
                        break;
                }
                break;

            case StarShape.Branch2_Straight:
                switch (standardBranch)
                {
                    case StandardBranch.Top:
                        standardBranch = StandardBranch.Right;
                        StarBlockBranchs[0] = RightVector;
                        StarBlockBranchs[1] = LeftVector;
                        break;

                    case StandardBranch.Right:
                        standardBranch = StandardBranch.Bottom;
                        StarBlockBranchs[0] = BottomVector;
                        StarBlockBranchs[1] = TopVector;
                        break;

                    case StandardBranch.Bottom:
                        standardBranch = StandardBranch.Left;
                        StarBlockBranchs[0] = LeftVector;
                        StarBlockBranchs[1] = RightVector;
                        break;

                    case StandardBranch.Left:
                        standardBranch = StandardBranch.Top;
                        StarBlockBranchs[0] = TopVector;
                        StarBlockBranchs[1] = BottomVector;
                        break;
                }
                break;

            case StarShape.Branch3:
                switch (standardBranch)
                {
                    case StandardBranch.Top:
                        standardBranch = StandardBranch.Right;
                        StarBlockBranchs[0] = RightVector;
                        StarBlockBranchs[1] = BottomVector;
                        StarBlockBranchs[2] = TopVector;
                        break;

                    case StandardBranch.Right:
                        standardBranch = StandardBranch.Bottom;
                        StarBlockBranchs[0] = BottomVector;
                        StarBlockBranchs[1] = LeftVector;
                        StarBlockBranchs[2] = RightVector;
                        break;

                    case StandardBranch.Bottom:
                        standardBranch = StandardBranch.Left;
                        StarBlockBranchs[0] = LeftVector;
                        StarBlockBranchs[1] = TopVector;
                        StarBlockBranchs[2] = BottomVector;
                        break;

                    case StandardBranch.Left:
                        standardBranch = StandardBranch.Top;
                        StarBlockBranchs[0] = TopVector;
                        StarBlockBranchs[1] = RightVector;
                        StarBlockBranchs[2] = LeftVector;
                        break;
                }
                break;
        }
    }

    private IEnumerator BomebStarBlock() // ����
    {
        GetComponent<SpriteRenderer>().enabled = false;

        ComboManager.Instance.IncreaseCombo();

        Blink();
        if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Scorpio) && starType.Equals(StarType.DontMove))
            StarBoard.Instance.DontMoveStarBlockNum--;
        
        if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Sagittarius) && starType.Equals(StarType.Cancer))
            StarBoard.Instance.CancerStarBlockNum--;

        if (GameManager.Instance.CurrentChapter.Equals(GameManager.ChapterType.Aquarius) && starType.Equals(StarType.DoubleBomb))
            StarBoard.Instance.DoubleBombStarBlockNum--;

        StartCoroutine(StarBoard.Instance.PlacedStarBlcok(transform, MyPosition, 0.5f));

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void Blink()
    {
        foreach(Vector3 StarBlockBranch in StarBlockBranchs)
        {
            RaycastHit2D[] Hits = Physics2D.RaycastAll(transform.position, StarBlockBranch, 0.5f, LayerMask.GetMask("StarBlockCheckBox"));
            foreach (RaycastHit2D Hit in Hits)
                if (Hit.collider != null)
                     Hit.transform.tag = "Link";
        }
    }

    public void ChangeStarType(StarType starType)
    {
        switch (starType)
        {
            case StarType.Cancer:
                if (!this.starType.Equals(StarType.Cancer))
                {
                    StarBoard.Instance.InstantiateStarBlock("Prefabs/StarBlocks/StarBlock_Cancer_Branch1", transform, MyPosition);
                    Destroy(this.gameObject);
                }
                break;
        }
    }
}

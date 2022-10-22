using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StarBlock : Block
{
    public enum Types { Standard, DontMove, DoubleClick, DoubleBomb }
    [SerializeField] private Types Type;
    public Types GetTypes { get { return Type; } }
    public enum StarState { Waiting, Moving, Bomb }
    [HideInInspector] public StarState starState;

    [HideInInspector] public int MyPosition;

    private float currentrotAngle;
    private float rotAngle;

    private bool IsClicked = false;

    protected override void Awake()
    {
        starState = StarState.Waiting;

        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (starState == StarState.Bomb && !IsBomb)
        {
            // 두번 터짐 처리
            if (Type == Types.DoubleBomb)
            {
                Blink();
                StarBoard.Instance.DoubleBombStarBlockNum--;
                Type = Types.Standard;
                starState = StarState.Waiting;
                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).tag = "Untagged";
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/S_StarBlock_Branch" + ((int)Shape + 1).ToString());
            }
            else
            {
                StartCoroutine(BomebStarBlock());
            }
        }
    }

    private void OnMouseDown()
    {
        if (!InGameManager.Instance.IsPaused && starState.Equals(StarState.Waiting))
        {
            // 움직이지 않는 블럭 처리
            if (!Type.Equals(Types.DontMove))
            {
                // 더블클릭 처리
                if (Type.Equals(Types.DoubleClick))
                {
                    if (IsClicked)
                    {
                        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/S_StarBlock_DC_1_Branch" + ((int)Shape + 1).ToString());
                        SoundManager.Instance.PlaySFXSound("A_StarBlock");
                        starState = StarState.Moving;
                        StartCoroutine(RotationStarBlock());
                    }
                    else
                        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/S_StarBlock_DC_2_Branch" + ((int)Shape + 1).ToString());
                    IsClicked = !IsClicked;
                }
                else
                {
                    SoundManager.Instance.PlaySFXSound("A_StarBlock");
                    starState = StarState.Moving;
                    StartCoroutine(RotationStarBlock());
                }
            }
        }
    }

    private IEnumerator RotationStarBlock() // starblock을 회전시키는 함수
    {
        currentrotAngle = transform.rotation.eulerAngles.z;         // 현재 로테이션 z를 받아옴(오일러 transfrom으로 받아옴)
        rotAngle = currentrotAngle - 90f;           // 90도 돌아간 로테이션 설정
        while (rotAngle < currentrotAngle)          // 목표 로테이션에 도달할때가지 반복
        {
            currentrotAngle -= 10f;                  // 회전 속도 설정
            if (rotAngle > currentrotAngle)         // 연산 중 목표 로테이션을 넘었을 시 발동
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotAngle);    // 최종 로테이션으로 설정
                break;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, currentrotAngle); // 회전
            yield return null;        // 일정 시간마다 실행
        }
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotAngle);    // 목표 로테이션 설정

        ChangeTopBranch(); // 막대기의 위치 변경

        starState = StarState.Waiting;
    }

    // 폭발 시작
    private IEnumerator BomebStarBlock()
    {
        IsBomb = true;
        GetComponent<SpriteRenderer>().enabled = false;

        ComboManager.Instance.IncreaseCombo();

        Blink();

        StartCoroutine(StarBoard.Instance.PlaceStarBlcok(transform, MyPosition, 0.5f));

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    // 반짝임
    private void Blink()
    {
        for (int i = 0; i < 4; i++)
        {
            if (BlockBranchs[i])
            {
                RaycastHit2D[] Hits = Physics2D.RaycastAll(transform.position, TRBLVector[i], 0.5f, LayerMask.GetMask("StarBlockCheckBox"));
                foreach (RaycastHit2D Hit in Hits)
                    if (Hit.collider != null)
                    {
                        if (Type == Types.DoubleBomb)
                            Hit.transform.tag = "FakeLink";
                        else
                            Hit.transform.tag = "Link";
                    }
            }
        }
    }
}

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
            // �ι� ���� ó��
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
            // �������� �ʴ� �� ó��
            if (!Type.Equals(Types.DontMove))
            {
                // ����Ŭ�� ó��
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

    private IEnumerator RotationStarBlock() // starblock�� ȸ����Ű�� �Լ�
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

        ChangeTopBranch(); // ������� ��ġ ����

        starState = StarState.Waiting;
    }

    // ���� ����
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

    // ��¦��
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

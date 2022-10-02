using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StarBlock : Block
{
    private enum Types { Standard, DontMove, DoubleClick, DoubleBomb }
    [SerializeField] private Types Type;
    public enum StarState { Waiting, Moving, Bomb }
    [HideInInspector] public StarState starState;

    [HideInInspector] public int MyPosition;

    private float currentrotAngle;
    private float rotAngle;

    private bool IsClicked = false;
    private bool IsBombOnce = false;

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
        if (starState.Equals(StarState.Bomb) && !IsBomb)
        {
            // �ι� ���� ó��
            if (Type == Types.DoubleBomb)
            {
                if (IsBombOnce)
                {
                    StartCoroutine(BomebStarBlock());
                }
                else
                {
                    for (int i = 0; i < transform.childCount; i++)
                        transform.GetChild(i).tag = "Untagged";
                    starState = StarState.Waiting;
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("S_StarBlock_Branch" + (int)Shape + 1);
                    IsBombOnce = true;
                }
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
                        starState = StarState.Moving;
                        StartCoroutine(RotationStarBlock());
                    }
                    IsClicked = !IsClicked;
                }
                else
                {
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

        if (Type == Types.DoubleBomb)
            StarBoard.Instance.DoubleBombStarBlockNum--;

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
                        Hit.transform.tag = "Link";
            }
        }
    }
}

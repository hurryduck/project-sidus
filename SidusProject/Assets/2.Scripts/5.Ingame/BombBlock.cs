using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBlock : Block
{
    public enum BlockStates { Waiting, Bomb }
    [HideInInspector] public BlockStates BlockState;

    [HideInInspector] public int MyPosition;
    [HideInInspector] public int HaveToTrue;

    [SerializeField] private GameObject BombImage;

    public int GetShape { get { return (int)Shape; } }
    [HideInInspector] public int RandomNum;
    public bool[] GetBranchs { get { return BlockBranchs; } }

    protected override void Awake()
    {
        BlockState = BlockStates.Waiting;
    }

    protected override void Start()
    {
        do
        {
            SetBranch();
            RandomNum = Random.Range(0, 4);
            for (int i = 0; i < RandomNum; i++)
                ChangeTopBranch();  // ������ üũ �ڽ��� ȸ�� ��Ŵ
        } while (!BlockBranchs[HaveToTrue]);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, RandomNum * -90); // ������ ȸ��
    }

    public void Bomb()
    {
        BombImage.SetActive(true);
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        StartCoroutine(StarBoard.Instance.PlaceBombBlock(transform, MyPosition, HaveToTrue, 1));
        RaycastHit2D[] Hits = Physics2D.RaycastAll(transform.position, TRBLVector[HaveToTrue], 0.5f, LayerMask.GetMask("StarBlockCheckBox"));
        foreach (RaycastHit2D Hit in Hits)
            if (Hit.collider != null)
                Hit.transform.tag = "Link";
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

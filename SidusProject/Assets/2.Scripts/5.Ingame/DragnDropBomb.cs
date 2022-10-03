using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDropBomb : MonoBehaviour
{
    private static DragnDropBomb instance;
    public static DragnDropBomb Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<DragnDropBomb>();
            return instance;
        }
    }

    [SerializeField] private SpriteRenderer BombSprite;
    private Vector3 StartPos;
    private bool IsBeingHeld = false;

    // ArrayNum�� Ȱ���ϴ� ������ �������� �����Ǵ� ��ź�� �������� �����Ǵ� ��źƲ �߿� �´� ���� ���� ��츦 �����ϱ� ���ؼ� ����Ѵ�.
    private int ArrayNum = -1;
    private int RandomNum;

    private enum Shapes { Branch1, Branch2, Branch3 }
    private Shapes Shape;
    public bool[] Branchs = new bool[4];

    private void SetBranch()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        switch (Shape)
        {
            #region ����
            case Shapes.Branch1:
                BombSprite.sprite = Resources.Load<Sprite>("Sprites/S_Bomb_Branch1");
                Branchs[0] = true;   // Top
                Branchs[1] = false;  // Right
                Branchs[2] = true;   // Bottom
                Branchs[3] = false;  // Left
                break;

            case Shapes.Branch2:
                BombSprite.sprite = Resources.Load<Sprite>("Sprites/S_Bomb_Branch2");
                Branchs[0] = true;
                Branchs[1] = true;
                Branchs[2] = false;
                Branchs[3] = false;
                break;

            case Shapes.Branch3:
                BombSprite.sprite = Resources.Load<Sprite>("Sprites/S_Bomb_Branch3");
                Branchs[0] = true;
                Branchs[1] = true;
                Branchs[2] = false;
                Branchs[3] = true;
                break;
                #endregion
        }
    }

    private void ChangeTopBranch()
    {
        bool[] temp = new bool[4];
        temp[0] = Branchs[3];
        temp[1] = Branchs[0];
        temp[2] = Branchs[1];
        temp[3] = Branchs[2];
        Branchs = temp;
    }

    public void SetBomb()
    {
        // �ֵ����ڸ��� ��� ��ġ �Ǵ� ���� �����Ǿ� ����
        int Range;
        if (GameManager.Instance.CurrentChapter == GameManager.ChapterType.Gemini)
            Range = 16;
        else
            Range = 8;

        // ���� ���� ��źƲ�� ������ �� ��� ��źƲ�� ���� �ϸ� �´� ��źƲ�� �������� ���� �����ϱ� ���� �Ʒ� ������ �����Ѵ�.
        if (ArrayNum != -1)
        {
            int temp = Random.Range(0, Range);
            while (temp == ArrayNum) temp = Random.Range(0, Range);
            ArrayNum = temp;
        }
        else
            ArrayNum = Random.Range(0, Range);

        BombBlock bomb = StarBoard.Instance.BombTiles[ArrayNum].bombBlock;
        Shape = (Shapes)bomb.GetShape;

        SetBranch();
        RandomNum = bomb.RandomNum;
        for (int i = 0; i < RandomNum; i++)
            ChangeTopBranch();
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, bomb.RandomNum * -90); // ������ ȸ��
    }

    private void Start()
    {
        StartPos = transform.position;
        Invoke("SetBomb", 0.01f);
    }

    private void Update()
    {
        if (IsBeingHeld)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector2(mousePos.x, mousePos.y);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsBeingHeld = true;
            BombSprite.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    private void OnMouseUp()
    {
        IsBeingHeld = false;
        BombSprite.color = new Color(1f, 1f, 1f, 1f);

        Collider2D collision = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("BombBlockCheckBox"));
        if (collision != null)
        {
            BombBlock bombBlock = collision.transform.GetComponent<BombBlock>();
            if (Shape == Shapes.Branch1)
            {
                if (bombBlock.GetBranchs[0] == Branchs[0] 
                    && bombBlock.GetBranchs[1] == Branchs[1]
                    && bombBlock.GetBranchs[2] == Branchs[2]
                    && bombBlock.GetBranchs[3] == Branchs[3])
                {
                    bombBlock.Bomb();
                    SetBomb();
                }
            }
            else
            {
                if (bombBlock.RandomNum == RandomNum)
                {
                    bombBlock.Bomb();
                    SetBomb();
                }
            }
        }

        transform.position = StartPos;
    }


}

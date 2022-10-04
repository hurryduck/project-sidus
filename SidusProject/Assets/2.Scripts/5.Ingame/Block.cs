using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected enum Shapes { Branch1, Branch2, Branch3 }
    [SerializeField] protected Shapes Shape;

    // 가지 위치 선정
    protected bool[] BlockBranchs = new bool[4];
    protected Vector3[] TRBLVector =
        { new Vector3(0, 1, 0),     // Top
        new Vector3(1, 0, 0),       // Right
        new Vector3(0, -1, 0),      // Bottom
        new Vector3(-1, 0, 0) };    // Left

    protected bool IsBomb = false;

    protected virtual void Awake()
    {
        SetBranch();
    }

    protected virtual void Start()
    {
        // 별블록 랜덤으로 회전
        int RandomNum = Random.Range(0, 4);
        for (int i = 0; i < RandomNum; i++)
            ChangeTopBranch();  // 별블럭의 체크 박스를 회전 시킴
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, RandomNum * -90); // 별블럭을 회전
    }

    protected void SetBranch()
    {
        switch (Shape)
        {
            case Shapes.Branch1:
                BlockBranchs[0] = true;   // Top
                BlockBranchs[1] = false;  // Right
                BlockBranchs[2] = true;   // Bottom
                BlockBranchs[3] = false;  // Left
                break;

            case Shapes.Branch2:
                BlockBranchs[0] = true;
                BlockBranchs[1] = true;
                BlockBranchs[2] = false;
                BlockBranchs[3] = false;
                break;

            case Shapes.Branch3:
                BlockBranchs[0] = true;
                BlockBranchs[1] = true;
                BlockBranchs[2] = false;
                BlockBranchs[3] = true;
                break;
        }
    }

    protected void ChangeTopBranch()
    {
        bool[] temp = new bool[4];
        temp[0] = BlockBranchs[3];
        temp[1] = BlockBranchs[0];
        temp[2] = BlockBranchs[1];
        temp[3] = BlockBranchs[2];
        BlockBranchs = temp;
    }
}

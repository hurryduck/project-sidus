using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AskButton : MonoBehaviour
{
    private DialogScript Parent;

    [SerializeField] private int MyNum;
    [SerializeField] private Text Speech;

    public void SetActive(bool Isture, DialogScript Parent)
    {
        gameObject.SetActive(Isture);
        this.Parent = Parent;

        Speech.text = Parent.Datas[Parent.OderNum + MyNum - 1].Speech;
    }

    public void _Click()
    {
        Parent.SelectNum = MyNum;
    }
}

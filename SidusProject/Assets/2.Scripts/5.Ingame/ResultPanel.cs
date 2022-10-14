using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private Image ClearFail;
    [SerializeField] private Image[] Stars;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject ExitButton;

    public void SetPanel(bool isclear)
    {
        if (isclear)
        {
            ClearFail.sprite = Resources.Load<Sprite>("Sprites/S_W_Clear");
            RestartButton.SetActive(false);
            ExitButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 80);
        }
        else
        {

        }
    }
}

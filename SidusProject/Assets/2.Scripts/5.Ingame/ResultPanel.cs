using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private Image ClearFail;
    [SerializeField] private Image[] Stars;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject NextButton;

    public void SetPanel(bool isclear)
    {
        if (isclear)
        {
            SoundManager.Instance.PlaySFXSound("A_Effect_ClearStage");
            ClearFail.sprite = Resources.Load<Sprite>("Sprites/S_W_Clear");
            RestartButton.SetActive(false);
            NextButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 80);

            int ClearTime = Mathf.FloorToInt(TimerManager.Instance.CurrentTime);
            if(ClearTime >= 120)
            {
                Stars[0].sprite = Resources.Load<Sprite>("Sprites/S_SmallStar_ON");
                Stars[1].sprite = Resources.Load<Sprite>("Sprites/S_BigStar_ON");
                Stars[2].sprite = Resources.Load<Sprite>("Sprites/S_SmallStar_ON");
            }
            else if(ClearTime >= 60)
            {
                Stars[0].sprite = Resources.Load<Sprite>("Sprites/S_SmallStar_ON");
                Stars[1].sprite = Resources.Load<Sprite>("Sprites/S_BigStar_ON");
            }
            else
                Stars[0].sprite = Resources.Load<Sprite>("Sprites/S_SmallStar_ON");
        }

        gameObject.SetActive(true);
    }
}

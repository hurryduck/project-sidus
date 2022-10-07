using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Locked : MonoBehaviour
{
    [SerializeField] private int MyValue;
    [SerializeField] private Image ParentButton;

    public enum CompareTarget { Chapter, Stage }
    [SerializeField] private CompareTarget compareTarget;

    [SerializeField] public GameObject WarningPanel;

    void Start()
    {
        switch (compareTarget)
        {
            case CompareTarget.Chapter:
                // 잠금해제
                if (MyValue <= GameManager.Instance.PlayerData.ClearChapterNum + 1)
                {
                    GetComponent<Image>().raycastTarget = false;
                    gameObject.SetActive(false);
                }
                // 잠금
                else
                {
                    ParentButton.raycastTarget = false;
                    GetComponent<Image>().raycastTarget = true;
                    ParentButton.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                }
                break;

            case CompareTarget.Stage:
                // 잠금해제
                if (MyValue < GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] + 1)
                {
                    ParentButton.gameObject.GetComponent<StageButton>().ChangeSprite(StageButton.StageButtonState.Cleared);
                }
                // 클리어해야하는 경우
                else if (MyValue == GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] + 1)
                {
                    ParentButton.gameObject.GetComponent<StageButton>().ChangeSprite(StageButton.StageButtonState.Clear);
                }
                // 잠금
                else
                {
                    ParentButton.gameObject.GetComponent<StageButton>().ChangeSprite(StageButton.StageButtonState.Locked);
                    ParentButton.raycastTarget = false;
                }
                break;
        }
    }

    public void _Click()
    {
        WarningPanel.SetActive(true);
    }
}

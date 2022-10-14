using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance;
    public static InGameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<InGameManager>();
            return instance;
        }
    }

    [SerializeField] private ResultPanel ResultMenuUI;
    [SerializeField] private Button ExitButton;

    [HideInInspector] public bool IsPaused = false;

    public void EndGame(bool IsClear = false)
    {
        ResultMenuUI.SetPanel(IsClear);
        TimerManager.Instance.EndTimer = true;

        if (IsClear)
        {
            // 클리어한 스테이지 보다 현재 스테이지가 높을 경우
            if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] < GameManager.Instance.CurrentStage)
                GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] = GameManager.Instance.CurrentStage;
            ExitButtonOnClick();
        }
    }

    private void ExitButtonOnClick()
    {
        switch (GameManager.Instance.CurrentChapter)
        {
            case GameManager.ChapterType.Aries:
                if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 5)
                {
                    ExitButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("4.Talk"); });
                    GameManager.Instance.PlayerData.ClearChapterNum++;
                }
                else
                    ExitButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter01_Stages"); });

                break;
            case GameManager.ChapterType.Taurus:
                if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 10)
                {
                    ExitButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("4.Talk"); });
                    GameManager.Instance.PlayerData.ClearChapterNum++;
                }
                else
                    ExitButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter02_Stages"); });
                break;

            case GameManager.ChapterType.Gemini:
                break;

            case GameManager.ChapterType.Cancer:
                break;

            case GameManager.ChapterType.Leo:
                break;

            case GameManager.ChapterType.Virgo:
                break;

        }

        GameManager.Instance.SaveData();
    }
}

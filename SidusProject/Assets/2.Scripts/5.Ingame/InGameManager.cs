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
    [SerializeField] private Button NextButton;

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
                if (GameManager.Instance.CurrentStage == 5)
                {
                    if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 5)
                    {
                        NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("4.Talk"); });
                        if (GameManager.Instance.PlayerData.ClearChapterNum < (int)GameManager.Instance.CurrentChapter)
                            GameManager.Instance.PlayerData.ClearChapterNum++;
                    }
                }
                else
                    NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter01_Stages"); });
                break;

            case GameManager.ChapterType.Taurus:
                if (GameManager.Instance.CurrentStage == 10)
                {
                    if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 10)
                    {
                        NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("4.Talk"); });
                        if (GameManager.Instance.PlayerData.ClearChapterNum < (int)GameManager.Instance.CurrentChapter)
                            GameManager.Instance.PlayerData.ClearChapterNum++;
                    }

                }
                else
                    NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter02_Stages"); });
                break;

            case GameManager.ChapterType.Gemini:
                if (GameManager.Instance.CurrentStage == 10)
                {
                    if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 10)
                    {
                        NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("4.Talk"); });
                        if (GameManager.Instance.PlayerData.ClearChapterNum < (int)GameManager.Instance.CurrentChapter)
                            GameManager.Instance.PlayerData.ClearChapterNum++;
                    }

                }
                else
                    NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter03_Stages"); });
                break;

            case GameManager.ChapterType.Cancer:
                if (GameManager.Instance.CurrentStage == 5)
                {
                    if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 5)
                    {
                        NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("4.Talk"); });
                        if (GameManager.Instance.PlayerData.ClearChapterNum < (int)GameManager.Instance.CurrentChapter)
                            GameManager.Instance.PlayerData.ClearChapterNum++;
                    }

                }
                else
                    NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter04_Stages"); });
                break;

            case GameManager.ChapterType.Leo:
                if (GameManager.Instance.CurrentStage == 9)
                {
                    if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 9)
                    {
                        NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("4.Talk"); });
                        if (GameManager.Instance.PlayerData.ClearChapterNum < (int)GameManager.Instance.CurrentChapter)
                            GameManager.Instance.PlayerData.ClearChapterNum++;
                    }

                }
                else
                    NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter05_Stages"); });
                break;

            case GameManager.ChapterType.Virgo:
                if (GameManager.Instance.CurrentStage == 10)
                {
                    if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 10)
                    {
                        NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("4.Talk"); });
                        if (GameManager.Instance.PlayerData.ClearChapterNum < (int)GameManager.Instance.CurrentChapter)
                            GameManager.Instance.PlayerData.ClearChapterNum++;
                    }

                }
                else
                    NextButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter06_Stages"); });
                break;
        }

        GameManager.Instance.SaveData();
    }
}

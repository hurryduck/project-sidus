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
            // Ŭ������ �������� ���� ���� ���������� ���� ���
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkSceneManager : MonoBehaviour
{
    [SerializeField] private Image Background;
    [SerializeField] private Image ChapterName;
    [SerializeField] private DialogScript Dialog;
    [SerializeField] private SkipAllButton SkipAll;

    private void Start()
    {
        switch (GameManager.Instance.CurrentChapter)
        {
            case GameManager.ChapterType.Aries:
                SetTalkScene(5, "Sprites/S_P_BG_01_Aries", "Sprites/S_CN_Aries", "Sprites/Character/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Taurus:
                SetTalkScene(10, "Sprites/S_P_BG_02_Taurus", "Sprites/S_CN_Taurus", "Sprites/Character/S_C_Taurus_Standing1");
                break;

            case GameManager.ChapterType.Gemini:
                SetTalkScene(10, "Sprites/S_P_BG_03_Gemini", "Sprites/S_CN_Gemini", "Sprites/Character/S_C_Gemini_Standing1");
                break;

            case GameManager.ChapterType.Cancer:
                SetTalkScene(5, "Sprites/S_P_BG_04_Cancer", "Sprites/S_CN_Cancer", "Sprites/Character/S_C_Dub_Standing1");
                break;

            case GameManager.ChapterType.Leo:
                SetTalkScene(9, "Sprites/S_P_BG_05_Leo", "Sprites/S_CN_Leo", "Sprites/Character/S_C_Urgula_Standing1");
                break;

            case GameManager.ChapterType.Virgo:
                SetTalkScene(10, "Sprites/S_P_BG_06_Virgo", "Sprites/S_CN_Virgo", "Sprites/Character/S_C_Ave_Standing1");
                break;
        }

        StartCoroutine(Dialog.Dialog());
        SkipAll.SetActive(Dialog.IsBefore);
    }

    private void SetTalkScene(int StageNum, string BG, string ChapterName, string ActorName)
    {
        if (GameManager.Instance.CurrentStage == StageNum &&
            GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == StageNum)
            Dialog.IsBefore = true;

        Background.sprite = Resources.Load<Sprite>(BG);
        this.ChapterName.sprite = Resources.Load<Sprite>(ChapterName);
        Dialog.Actor.sprite = Resources.Load<Sprite>(ActorName);
    }
}
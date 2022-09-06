using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkSceneManager : MonoBehaviour
{
    [SerializeField] private Image Background;
    [SerializeField] private Text ChapterName;
    [SerializeField] private DialogScript Dialog;
    [SerializeField] private SkipAllButton SkipAll;

    private void Start()
    {
        switch (GameManager.Instance.CurrentChapter)
        {
            case GameManager.ChapterType.Aries:
                SetTalkScene(5, "Sprites/S_BG_01_Aries", "양자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Taurus:
                SetTalkScene(10, "Sprites/S_BG_02_Taurus", "황소자리", "Sprites/S_C_Taurus_Standing1");
                break;

            case GameManager.ChapterType.Gemini:
                SetTalkScene(5, "Sprites/S_BG_03_Gemini", "쌍둥이자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Cancer:
                SetTalkScene(5, "Sprites/S_BG_04_Cancer", "게자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Leo:
                SetTalkScene(5, "Sprites/S_BG_05_Leo", "사자자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Virgo:
                SetTalkScene(5, "Sprites/S_BG__06Virgo", "처녀자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Libra:
                SetTalkScene(5, "Sprites/S_BG__07Libra", "천칭자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Scorpio:
                SetTalkScene(5, "Sprites/S_BG_08_Scorpio", "전갈자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Sagittarius:
                SetTalkScene(5, "Sprites/S_BG_09_Sagittarius", "궁수자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Capricorn:
                SetTalkScene(5, "Sprites/S_BG_10_Capricorn", "염소자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Aquarius:
                SetTalkScene(5, "Sprites/S_BG_11_Aquarius", "물병자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Pisces:
                SetTalkScene(5, "Sprites/S_BG_12_Pisces", "물고기자리", "Sprites/S_C_Kumal_Standing1");
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
        this.ChapterName.text = ChapterName;
        Dialog.Actor.sprite = Resources.Load<Sprite>(ActorName);
    }
}
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
                SetTalkScene(5, "Sprites/S_BG_Aries", "양자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Taurus:
                SetTalkScene(10, "Sprites/S_BG_Taurus", "황소자리", "Sprites/S_C_Taurus_Standing1");
                break;

            case GameManager.ChapterType.Gemini:
                SetTalkScene(5, "Sprites/S_BG_Gemini", "쌍둥이자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Cancer:
                SetTalkScene(5, "Sprites/S_BG_Cancer", "게자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Leo:
                SetTalkScene(5, "Sprites/S_BG_Leo", "사자자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Virgo:
                SetTalkScene(5, "Sprites/S_BG_Virgo", "처녀자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Libra:
                SetTalkScene(5, "Sprites/S_BG_Libra", "천칭자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Scorpio:
                SetTalkScene(5, "Sprites/S_BG_Scorpio", "전갈자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Sagittarius:
                SetTalkScene(5, "Sprites/S_BG_Sagittarius", "궁수자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Capricorn:
                SetTalkScene(5, "Sprites/S_BG_Capricorn", "염소자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Aquarius:
                SetTalkScene(5, "Sprites/S_BG_Aquarius", "물병자리", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Pisces:
                SetTalkScene(5, "Sprites/S_BG_Pisces", "물고기자리", "Sprites/S_C_Kumal_Standing1");
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
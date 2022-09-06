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
                SetTalkScene(5, "Sprites/S_BG_01_Aries", "���ڸ�", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Taurus:
                SetTalkScene(10, "Sprites/S_BG_02_Taurus", "Ȳ���ڸ�", "Sprites/S_C_Taurus_Standing1");
                break;

            case GameManager.ChapterType.Gemini:
                SetTalkScene(5, "Sprites/S_BG_03_Gemini", "�ֵ����ڸ�", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Cancer:
                SetTalkScene(5, "Sprites/S_BG_04_Cancer", "���ڸ�", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Leo:
                SetTalkScene(5, "Sprites/S_BG_05_Leo", "�����ڸ�", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Virgo:
                SetTalkScene(5, "Sprites/S_BG__06Virgo", "ó���ڸ�", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Libra:
                SetTalkScene(5, "Sprites/S_BG__07Libra", "õĪ�ڸ�", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Scorpio:
                SetTalkScene(5, "Sprites/S_BG_08_Scorpio", "�����ڸ�", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Sagittarius:
                SetTalkScene(5, "Sprites/S_BG_09_Sagittarius", "�ü��ڸ�", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Capricorn:
                SetTalkScene(5, "Sprites/S_BG_10_Capricorn", "�����ڸ�", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Aquarius:
                SetTalkScene(5, "Sprites/S_BG_11_Aquarius", "�����ڸ�", "Sprites/S_C_Kumal_Standing1");
                break;

            case GameManager.ChapterType.Pisces:
                SetTalkScene(5, "Sprites/S_BG_12_Pisces", "������ڸ�", "Sprites/S_C_Kumal_Standing1");
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
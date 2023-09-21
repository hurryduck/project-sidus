using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Image BGImage;

    private void Start()
    {
        switch (GameManager.Instance.CurrentChapter)
        {
            case GameManager.ChapterType.Aries:
                if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 5)
                    BGImage.sprite = Resources.Load<Sprite>("Sprites/S_BG_01_Aries_A");
                break;

            case GameManager.ChapterType.Taurus:
                if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 10)
                    BGImage.sprite = Resources.Load<Sprite>("Sprites/S_BG_02_Taurus_A");
                break;

            case GameManager.ChapterType.Gemini:
                if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 10)
                    BGImage.sprite = Resources.Load<Sprite>("Sprites/S_BG_03_Gemini_A");
                break;

            case GameManager.ChapterType.Cancer:
                if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 5)
                    BGImage.sprite = Resources.Load<Sprite>("Sprites/S_BG_04_Cancer_A");
                break;

            case GameManager.ChapterType.Leo:
                if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 9)
                    BGImage.sprite = Resources.Load<Sprite>("Sprites/S_BG_05_Leo_A");
                break;

            case GameManager.ChapterType.Virgo:
                if (GameManager.Instance.PlayerData.ClearStageNum[(int)GameManager.Instance.CurrentChapter] == 10)
                    BGImage.sprite = Resources.Load<Sprite>("Sprites/S_BG_06_Virgo_A");
                break;
        }
    }
}


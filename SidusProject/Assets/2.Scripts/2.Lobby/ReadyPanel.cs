using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyPanel : MonoBehaviour
{
    [SerializeField] private Text ChapterTitle;
    [SerializeField] private Image CharacterImage;
    [SerializeField] private Text PlaneName;
    [SerializeField] private Button PlayButton;

    private void OnEnable()
    {
        ReadyPanel_Data Data = DataTalbeManager.Instance.GetData_ReadyPanel((int)GameManager.Instance.CurrentChapter);

        ChapterTitle.text = '<' + Data.ChapterTitle + '>';
        CharacterImage.sprite = Resources.Load<Sprite>("Sprites/" + Data.CharacterImage);
        PlaneName.text = Data.PlaneName;

        switch (GameManager.Instance.CurrentChapter)
        {
            case GameManager.ChapterType.Aries:
                PlayButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter01_Stages"); });
                break;

            case GameManager.ChapterType.Taurus:
                PlayButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter02_Stages"); });
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
    }
}

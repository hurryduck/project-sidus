using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfoPanel : MonoBehaviour
{
    [SerializeField] private Text ChapterTitle;
    [SerializeField] private Image CharacterImage;
    [SerializeField] private Text PlaneName;
    [SerializeField] private Button PlayButton;

    [SerializeField] private Image PlanetImage;
    [SerializeField] private Image[] Puzzles;

    private void OnEnable()
    {
        PlanetInfo Data = DataTalbeManager.Instance.GetData_ReadyPanel((int)GameManager.Instance.CurrentChapter);

        ChapterTitle.text = Data.ChapterTitle;
        CharacterImage.sprite = Resources.Load<Sprite>("Sprites/" + Data.CharacterImage);
        PlaneName.text = '<' + Data.PlaneName + '>';
        PlayButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter0" + Data.StageNum + "_Stages"); });

        if (GameManager.Instance.PlayerData.ClearChapterNum >= Data.StageNum)
            PlanetImage.sprite = Resources.Load<Sprite>("Sprites/Puzzle/S_BG_" + Data.StageNum + "_After");
        else
        {
            PlanetImage.sprite = Resources.Load<Sprite>("Sprites/Puzzle/S_BG_" + Data.StageNum + "_Before");
            int ClearStageNum = GameManager.Instance.PlayerData.ClearStageNum[Data.StageNum - 1];
            for (int i = 0; i < 10; i++)
            {
                if (i < ClearStageNum)
                {
                    Puzzles[i].sprite = Resources.Load<Sprite>("Sprites/Puzzle/S_P_" + Data.StageNum + "_" + (i + 1));
                    Puzzles[i].gameObject.SetActive(true);
                }
                else
                    Puzzles[i].gameObject.SetActive(false);
            }
        }

    }
}

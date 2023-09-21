using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField] public Image sprite;
    [SerializeField] private int StageNum;

    public enum StageButtonState { Locked, Clear, Cleared }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetStageNum);
    }

    private void SetStageNum()
    {
        GameManager.Instance.CurrentStage = StageNum;
    }

    public void ChangeSprite(StageButtonState state)
    {
        switch (state)
        {
            case StageButtonState.Locked:
                sprite.sprite = Resources.Load<Sprite>("Sprites/S_UI_Stage_1");
                break;

            case StageButtonState.Clear:
                Instantiate(Resources.Load("Prefabs/P_StarBlink"), transform);
                break;

            case StageButtonState.Cleared:
                sprite.sprite = Resources.Load<Sprite>("Sprites/S_UI_Stage_2");
                break;
        }
    }
}

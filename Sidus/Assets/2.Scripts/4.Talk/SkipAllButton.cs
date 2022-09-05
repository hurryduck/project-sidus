using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipAllButton : MonoBehaviour
{
    [SerializeField] private Button thisButton;
    [SerializeField] private GameObject Dialog;
    [SerializeField] private GameObject GameStartButton;
    private bool IsActive;

    public void SetActive(bool IsBefore)
    {
        if (GameManager.Instance.PlayerData.TalkOnce[(int)GameManager.Instance.CurrentChapter] >= 1)
        {
            IsActive = true;
            if (IsBefore)
                if (GameManager.Instance.PlayerData.TalkOnce[(int)GameManager.Instance.CurrentChapter] < 2)
                    IsActive = false;
                else
                    IsActive = true;

            if (IsActive)
            {
                gameObject.SetActive(true);
                if (IsBefore)
                    thisButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("2.Lobby"); });
                else
                    thisButton.onClick.AddListener(delegate { OnClick_After(); });
            }
        }
    }

    private void OnClick_After()
    {
        gameObject.SetActive(false);
        Dialog.SetActive(false);
        GameStartButton.SetActive(true);
    }
}

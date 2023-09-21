using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ExitButton;

    public void Start()
    {
        ExitButton.onClick.AddListener(delegate { ButtonScript._LoadSceneName("3.Chapter0" + ((int)GameManager.Instance.CurrentChapter + 1) + "_Stages"); });
    }
}

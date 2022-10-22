using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ScoreManager>();
            return instance;
        }
    }

    [SerializeField] private Image GaugeBarImage;
    private float CurrentFill;
    private float MaxValue;
    private float currnetValue;
    public float CurrnetValue
    {
        get { return currnetValue; }
        set
        {
            currnetValue = value;
            if (CurrnetValue >= MaxValue)
                InGameManager.Instance.EndGame(true);

            CurrentFill = currnetValue / MaxValue;
        }
    }

    private void Start()
    {
        MaxValue = ((int)GameManager.Instance.CurrentChapter + 1) * 1000;
    }

    private void Update()
    {
        if (CurrentFill != GaugeBarImage.fillAmount)
            GaugeBarImage.fillAmount = Mathf.Lerp(GaugeBarImage.fillAmount, CurrentFill, Time.deltaTime * 2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    private static TimerManager instance;
    public static TimerManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TimerManager>();
            return instance;
        }
    }

    [SerializeField]
    private Text TimerText;

    private float CurrentTime;
    [HideInInspector]
    public bool EndTimer;

    private void Start()
    {
        EndTimer = false;
        CurrentTime = 180f;
    }

    private void Update()
    {
        if (!EndTimer)
        {
            if (CurrentTime > 0)
            {
                if (GameManager.Instance.CurrentChapter == GameManager.ChapterType.Capricorn)
                    CurrentTime -= Time.deltaTime * 1.5f;
                else
                    CurrentTime -= Time.deltaTime;
            }
            else
            {
                CurrentTime = 0;
                EndTimer = true;
                InGameManager.Instance.EndGame();
            }

            TimerText.text = string.Format("{0:00} : {1:00}", (int)(CurrentTime / 60), (int)(CurrentTime % 60));
        }
    }
}

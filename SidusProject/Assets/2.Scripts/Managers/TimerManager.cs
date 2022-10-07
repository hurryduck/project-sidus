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

    [SerializeField] private Slider Timer;

    [HideInInspector] public float CurrentTime;
    private readonly float MaxTime = 180;
    [HideInInspector] public bool EndTimer;

    private void Start()
    {
        EndTimer = false;
        CurrentTime = MaxTime;
    }

    private void Update()
    {
        if (!EndTimer)
        {
            if (CurrentTime > 0)
            {
                CurrentTime -= Time.deltaTime;
                Timer.value = CurrentTime / MaxTime;
            }
            else
            {
                CurrentTime = 0;
                EndTimer = true;
                InGameManager.Instance.EndGame();
            }

        }
    }
}

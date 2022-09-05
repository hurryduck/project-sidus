using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private static ComboManager instance;
    public static ComboManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ComboManager>();

            return instance;
        }
    }

    private int currentCombo = 0;

    private Coroutine nowCoroutine;

    public void IncreaseCombo()
    {
        if(nowCoroutine != null)
            StopCoroutine(nowCoroutine);
        currentCombo += 1;
        nowCoroutine = StartCoroutine(ComboTimer());
    }

    private IEnumerator ComboTimer()
    {
        yield return new WaitForSeconds(0.1f);

        int num;
        if(currentCombo > 2)
        {
            num = currentCombo * 20;

        }
        else
        {
            num = currentCombo * 10;

        }
        ScoreManager.Instance.CurrnetValue += num;
        currentCombo = 0;
    }
}

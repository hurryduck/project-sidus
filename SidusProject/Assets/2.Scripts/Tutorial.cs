using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] Scenes;

    public IEnumerator StartTutorial()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < Scenes.Length; i++)
            yield return StartCoroutine(Scene(i));
        gameObject.SetActive(false);
    }

    private IEnumerator Scene(int index)
    {
        Scenes[index].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
                break;
            yield return null;
        }
        Scenes[index].SetActive(false);
    }
}

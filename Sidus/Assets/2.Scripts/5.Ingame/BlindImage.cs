using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindImage : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestoryWaitForSeconds());
    }

    private IEnumerator DestoryWaitForSeconds()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

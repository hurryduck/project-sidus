using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("2.Lobby");
        }
    }
}

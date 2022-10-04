using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCheckBox : MonoBehaviour
{
    [SerializeField] private GameObject starBlock;
    private StarBlock starBlockScript;

    void Awake()
    {
        starBlockScript = starBlock.GetComponent<StarBlock>();
    }

    void Update()
    {
        if (gameObject.CompareTag("Link"))
        {
            starBlockScript.starState = StarBlock.StarState.Bomb;
        }

        if (gameObject.CompareTag("FakeLink"))
        {
            if (starBlockScript.GetTypes == StarBlock.Types.DoubleBomb)
                starBlockScript.starState = StarBlock.StarState.Bomb;
            else
                gameObject.tag = "Untagged";
        }
    }
}

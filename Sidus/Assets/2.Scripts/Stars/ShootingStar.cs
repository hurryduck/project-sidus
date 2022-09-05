using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : MonoBehaviour
{


    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

        RaycastHit2D[] Hits = new RaycastHit2D[4];
        Hits[0] = Physics2D.Raycast(transform.position, new Vector3(1, 0, 0),  0.5f, LayerMask.GetMask("StarBlockCheckBox"));
        Hits[1] = Physics2D.Raycast(transform.position, new Vector3(-1, 0, 0), 0.5f, LayerMask.GetMask("StarBlockCheckBox"));
        Hits[2] = Physics2D.Raycast(transform.position, new Vector3(0, 1, 0),  0.5f, LayerMask.GetMask("StarBlockCheckBox"));
        Hits[3] = Physics2D.Raycast(transform.position, new Vector3(0, -1, 0), 0.5f, LayerMask.GetMask("StarBlockCheckBox"));
        for(int i = 0; i < 4; i++)
        {
            if( Hits[i].collider != null)
            {
                Hits[i].transform.tag = "Link";
            }
        }
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}

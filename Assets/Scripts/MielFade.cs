using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MielFade : MonoBehaviour
{
    IEnumerator OnCollisionEnter(Collision hit)

    {
        if (hit.gameObject.CompareTag("Fourmis"))
        { 
            yield return new WaitForSeconds(2);
            gameObject.SetActive(false);
        }


    }
}

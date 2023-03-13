using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MielPris : MonoBehaviour
{
    IEnumerator OnCollisionEnter(Collision hit)

    {
        if (hit.gameObject.CompareTag("Fourmis"))
        {   
            gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
        }
        

    }
}

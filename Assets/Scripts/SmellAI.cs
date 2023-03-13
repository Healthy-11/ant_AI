using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellAI : MonoBehaviour
{

    void SmellRange(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.SendMessage("AddDamage");
        }
    }

}

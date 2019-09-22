using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTriggerPatrol : MonoBehaviour
{
    public SphereCollider myCollider;
    public float agroRad;
    public AIIncludeMoveC aiGoodies;

    private void Start()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.radius = agroRad;
        myCollider.isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            aiGoodies.isplayerInBound = false;
        }
    }
}
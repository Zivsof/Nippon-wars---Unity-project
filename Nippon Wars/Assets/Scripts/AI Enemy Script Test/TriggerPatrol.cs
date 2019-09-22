using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPatrol : MonoBehaviour
{
    public NPCSimplePatrol enem_Pat;
    public SphereCollider myCollider;
    public float agroRad;
    public NPCSimplePatrol aiGoodies;

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
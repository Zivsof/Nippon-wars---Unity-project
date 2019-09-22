using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.GetComponent<DialogueTrigger>().TriggerDialogue();
            other.gameObject.GetComponent<PlayerState>().TakeDamageFromTrap();
            Destroy(this.gameObject);
        }
    }
}

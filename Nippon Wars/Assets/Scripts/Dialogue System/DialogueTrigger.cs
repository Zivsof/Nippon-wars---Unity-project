using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FWmanager.Instance.DialogStart = true;
        GameObject.FindWithTag("FWmanager").GetComponent<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Enemy"))
        {
            dialogue.isCombat = true;

            TriggerDialogue();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTutorialTrigger : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         this.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
      }
   }
}

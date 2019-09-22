using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject triggerColl;
    public string levelName;

    void Update()
    {
        if (triggerColl != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && !FWmanager.Instance.DialogStart)
            {
                if (triggerColl.GetComponent<DialogueTrigger>())
                {
                    interactWithEnemyNpc();
                }

                else if (triggerColl.GetComponent<ItemScript>())
                {
                    interactWithItem();
                }

                else if (triggerColl.GetComponent<RockShatter>())
                {
                    triggerColl.GetComponent<RockShatter>().Explosion();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC") || other.gameObject.CompareTag("Enemy") ||
            other.gameObject.CompareTag("Item")|| other.gameObject.CompareTag("BreakAble"))
        {
            triggerColl = other.gameObject;
            FWmanager.Instance.PlayerInteraction.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggerColl = null;
        FWmanager.Instance.PlayerInteraction.SetActive(false);
    }

    public void interactWithEnemyNpc()
    {
        var DT = triggerColl.GetComponent<DialogueTrigger>();
        if (triggerColl.CompareTag("Enemy"))
        {
            DT.dialogue.isCombat = true;
            //triggerColl.GetComponent<DialogueTrigger>().dialogue.isCombat = true;
            FWmanager.Instance.ChangeToLevel(levelName);
            FWmanager.Instance.GetEnemyId(triggerColl);
        }

        DT.TriggerDialogue();
        //triggerColl.GetComponent<DialogueTrigger>().TriggerDialogue();
        triggerColl = null;
    }

    public void interactWithItem()
    {
        var item = triggerColl.GetComponent<ItemScript>();
        var pState = this.GetComponent<PlayerState>();
        if (item.pickAble && pState.localPlayer.Potions < 3)
        {
            pState.grabPotion();
            useItem();
        }
        else
        {
            //eat the food and gain hp
            pState.eatFood(item.foodWorth);
            useItem();
        }
    }

    public void useItem()
    {
        FWmanager.Instance.GetItemId(triggerColl);
        Destroy(triggerColl.gameObject);
        triggerColl = null;
        FWmanager.Instance.PlayerInteraction.SetActive(false);
    }
}
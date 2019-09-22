using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    //At start, load data from GlobalControl.
    [Header("Player State Settings")]
    public PlayerStatistics localPlayer;
    [Header("Potion Settings")]
    public int potionValue;
    protected void Start()
    {
        if (GlobalController.Instance != null)
        {
            localPlayer = GlobalController.Instance.savedPlayerDate;
        }
       
    }

    public void SavePlayer()
    {
        GlobalController.Instance.savedPlayerDate = localPlayer;
    }

    public void eatFood(int foodValue)
    {
        float newHp = localPlayer.HP + foodValue;
        Debug.Log("going to eat food "+ foodValue +"  my hp is " + localPlayer.HP);
        // Opinion: we can add max hp to player Serializables.cs->PlayerStatistics class and then we can swap the 100 with player max hp if we need;
        if (newHp >= 100)
        {
            localPlayer.HP = 100;
        }
        else if (newHp < 100)
        {
            localPlayer.HP += foodValue;
        }
    }

    public void grabPotion()
    {
        localPlayer.Potions++;
    }

    public void DrinkPotion()
    {
        float newHp = localPlayer.HP + potionValue;
        // Opinion: we can add max hp to player Serializables.cs->PlayerStatistics class and then we can swap the 100 with player max hp if we need;
        if (newHp > 100)
        {
            localPlayer.HP = 100;    
        }
        else if (newHp < 100)
        {
            localPlayer.HP += potionValue;
        }

        localPlayer.Potions--;
    }

    public bool playerHavePotions()
    {
        return localPlayer.Potions != 0;
    }

    public void LevelUp()
    {
        // Opinion: we can add max hp to player Serializables.cs->PlayerStatistics class and we can increase the max hp every level up;
        localPlayer.powerAttack += 5;
        localPlayer.Armor += 5;
        localPlayer.level += 1;
        localPlayer.XP = localPlayer.XP %  100;
    }

    public void TakeDamageFromTrap()
    {
        localPlayer.HP -= 30;
    }
}
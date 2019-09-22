using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStatsUI : MonoBehaviour
{
    private PlayerState playerStatus;
    [Header("PlayerUI Texts")]
    public Text hpText;
    public Text powerAttackText;
    public Text armorText;
    public Text xpText;
    public Text levelText;
    public Text potionText;

    void Start()
    {
        playerStatus = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStatus != null)
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        hpText.text = "HP: " + playerStatus.localPlayer.HP + "/100";
        powerAttackText.text = "Power Attack: " + playerStatus.localPlayer.powerAttack;
        armorText.text = "Armor: " + playerStatus.localPlayer.Armor;
        // todo need to speak with guy about the exp balance for each level;
        xpText.text = "Exp: " + playerStatus.localPlayer.XP + "/100";
        levelText.text = "Level: " + playerStatus.localPlayer.level;
        potionText.text = "Potions: " + playerStatus.localPlayer.Potions + "/3";
    }
}
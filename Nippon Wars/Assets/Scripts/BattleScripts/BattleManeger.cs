using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BattleManeger : MonoBehaviour
{
    public static BattleManeger Instance;
    [HideInInspector] public int whichturn = 1; //1 - Player's Turn, 2 - Enemy Turn
    [HideInInspector] public bool playerDead = false;
    public bool PlayerWin = false;
    public PlayerState thePlayer;
    public EnemyState theEnemy;
    [Header("Enemy Spawner")] public Transform enemySpawner;
    public GameObject[] minions;
    [Header("Others")] public GameObject playerUI;
    public Text enemyName;
    public GameObject endGame;
    private float monsterExp;
    public Text endGameText;
    public Text runText;
    public GameObject levelSwaper;
    public GameObject playerPanel;
    [Header("Scene To Go Back")] 
    public string scenceLevelBack;

    /* Guy's addition - need to add for the Monster's animator */
    private Animator enemyAnimator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        if (GlobalController.Instance == null)
        {
            spawnEnemy(3);
        }
        else
        {
            spawnEnemy(GlobalController.Instance.enemyID);
        }


        if (theEnemy == null)
        {
            theEnemy = GameObject.FindWithTag("Enemy").GetComponent<EnemyState>();
        }


        if (thePlayer == null)
        {
            thePlayer = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        }

        thePlayer.gameObject.transform.position = new Vector3(0, 0, 0);


        /* Guy's addition - saving the enemy's animator */
        enemyAnimator = GameObject.FindWithTag("Enemy").GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //levelSwaper = GameObject.FindWithTag("LevelChanger");
        levelSwaper.SetActive(true);
        if (GlobalController.Instance != null)
        {
            GlobalController.Instance.battleModeActive = true;
            switch (GlobalController.Instance.sceneToReturn)
            {
                case 0:
                    scenceLevelBack = "Testing";
                    break;
                case 1:
                    scenceLevelBack = "Tutorial";
                    break;
                case 2:
                    scenceLevelBack = "WorldMap";
                    break;
            }
        }
        else
        {
            scenceLevelBack = "Testing";
        }

        enemyName.text = theEnemy.gameObject.name.Remove(theEnemy.gameObject.name.Length - 7);
        monsterExp = theEnemy.expWorth;
    }

    private void Update()
    {
        if (PlayerWin)
        {
            if (thePlayer.gameObject.GetComponent<CombatPlayer>().isAnimationIdle() &&PlayerWin)
            {
                PlayerWin = false;
                startVictory();
            }
        }

        if (whichturn != 1)
        {
            playerPanel.SetActive(false);
        }
        else
        {
            playerPanel.SetActive(true);
        }
    }

    /* 
     *  whichturn - 1 is the Player's turn, 2 is the Monster's turn. 
     */
    private void FixedUpdate()
    {
        if (whichturn == 1 || whichturn == 2) //Player's or Monster's turn
        {
            playerUI.SetActive(true);
        }
        else //otherwise, the player is dead.
        {
            playerUI.SetActive(false);
        }
    }
    

    public void AttackOnPlayer()
    {
        /* Guy's addition - Triggering an attack */
        //enemyAnimator.SetTrigger("LightAttack");
        

        float playerhp = thePlayer.localPlayer.HP;
        float currentDamage = 0;
        if (theEnemy.attackPower >= thePlayer.localPlayer.Armor)
        {
            currentDamage = theEnemy.attackPower - thePlayer.localPlayer.Armor;
        }
        else
        {
            currentDamage = 1;
        }

        if (playerhp > currentDamage)
        {
            thePlayer.localPlayer.HP -= (int)currentDamage;
            thePlayer.GetComponent<CombatPlayer>().timeHit = 1;
        }
        else
        {
            thePlayer.localPlayer.HP = 0;
            playerDead = true;
        }

        if (playerDead)
        {
            BattleManeger.Instance.whichturn = 0;
            GlobalController.Instance.playerDefeated = true;

            //todo
            //Battle Mode OVER give 2 second and back to the openworld to checkpoint
            // or more evil is to return to the main menu
        }
    }

    public void AttackOnEnemy(float damage)
    {
        this.gameObject.GetComponent<EnemyHealthBar>().ReciveDamage(damage);
        whichturn = 2;
    }

    private void startVictory()
    {
        PlayerWin = false;
        thePlayer.localPlayer.XP += theEnemy.expWorth;
        if ( thePlayer.localPlayer.XP <= 100)
        {
           thePlayer.LevelUp();
           endGameText.text = "You got " + monsterExp + " exp And LEVEL UP!!";
        }
        else
        {
            endGameText.text = "You got " + monsterExp + " exp";
        }
        // todo will be replace with trigger to death animation
        
        endGame.SetActive(true);
        BattleManeger.Instance.whichturn = 0;
        StartCoroutine(EndBattleMode());
        //levelSwaper.GetComponent<LevelChanger>().FadeToLevel(scenceLevelBack);
    }
    
    IEnumerator EndBattleMode()
    {
        yield return new WaitForSeconds(2);
        endGame.SetActive(false);
        enemyAnimator.SetTrigger("Die");
        yield return new WaitForSeconds(1);
        theEnemy.GetComponent<CombatEnemy>().KillYourSelf();
        yield return new WaitForSeconds(2);
        levelSwaper.GetComponent<LevelChanger>().FadeToLevel(scenceLevelBack);
    }

    public void RunFromBattle()
    {
        if (scenceLevelBack != "Tutorial")
        {
            levelSwaper.GetComponent<LevelChanger>().FadeToLevel(scenceLevelBack);
        }
        else
        {
            StartCoroutine(CantRun());
        }
    }

    IEnumerator CantRun()
    {
        runText.text = "Can't Run";
        yield return new WaitForSeconds(1);
        runText.text = "Run";
    }
    
    

    private void spawnEnemy(int enemyID)
    {
        switch (enemyID)
        {
            case 1:
                Instantiate(minions[0], enemySpawner);
                break;
            case 2:
                Instantiate(minions[1], enemySpawner);
                break;
            case 3:
                Instantiate(minions[2], enemySpawner);
                break;
        }
    }

    public void ChangeToLevel(string levelName)
    {
        levelSwaper.GetComponent<LevelChanger>().FadeToLevel("Tutorial");
    }
}
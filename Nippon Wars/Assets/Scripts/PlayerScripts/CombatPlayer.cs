using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatPlayer : MonoBehaviour
{
    /************ Start of Guy's Variables ************/
    /* Debug information */
    public bool showDebug = true;

    /* Attacking chance runs between 0 - 10 */
    private int chance;

    public int lightAttackChance = 8;
    public float lightAttackDamage = 10;

    public int heavyAttackChance = 5;
    public float heavyAttackDamage = 25;

    public int potionHealingPoints = 100;

    //public AnimationClip clip;
    public Animator playerAnimator;
    private Animator enemyAnimator;

    private Animator
        cinematicCameraAnimationController; //the "cinematic" camera - used to show cinematic view of the battle field.

    public GameObject UI; //the User Interface, mainly used to turn off and on the interface for the UI.

    public Camera mainCamera; //the main Camera is the main camera that shows the 'Default' battle field view.

    public Camera
        cinematicCamera; //The cinematic Camera always hovers on the battle field, capturing the battle in a cinematic shot.

    public LevelChanger lc;

    /************ End of Guy's Variables ************/


    private float powerAttack;
    private int playerPotions;
    public Text playerPotions_Text;
    public int timeHit = 1;

    public string BattleSceneName;
    public PlayerState player;

    public float AttackPower;

    /*
     in this script we shell manage the player to attack the enemy
     (animations and transfer Damage to enemy + grab from the player his power attack.
     i am not quite sure if here we can manage the die state 
     or build Main combat Script (GameManager script for battle mode).
    */
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Testing 2")
        {
            this.enabled = true;
        }
        else
        {
            this.enabled = false;
        }

        player = this.gameObject.GetComponent<PlayerState>();
        //player = this.gameObject.GetComponent<PlayerState>();
        if (SceneManager.GetActiveScene().name == BattleSceneName)
        {
            powerAttack = BattleManeger.Instance.thePlayer.localPlayer.powerAttack;
        }

        /***** getting the animator for the Enemy. *****/
        enemyAnimator = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<Animator>();
        if (SceneManager.GetActiveScene().name == "Testing 2")
        {
            /***** Start of Guy's start methods *****/
            cinematicCameraAnimationController = mainCamera.GetComponent<Animator>();
            playerPotions_Text.text = player.localPlayer.Potions.ToString();
            /***** End of Guy's start methods *****/
        }

        
    }

    /*
     * using FixedUpdate to check every few frames if the state is a Desicion-Stage or Result-Stage.
     */
    private void FixedUpdate()
    {
        if (showDebug)
        {
            Debug.Log("Animation is Idle: " + isAnimationIdle());
        }

        if (isAnimationIdle())
        {
            endBattlePhase();
        }
        else startBattlePhase();
    }

    //Light Attack - by default high chance to hit does 10 damage.
    public void playerAttack1()
    {
        if (BattleManeger.Instance.whichturn != 1) return;
        timeHit = 0;
        AttackPower = powerAttack;
        playerAnimator.SetTrigger("LightAttack");

        if (showDebug)
        {
            Debug.Log("LightAttack successed: " +
                      isAnimationRunning(playerAnimator.GetCurrentAnimatorStateInfo(0), "LightAttack"));
        }

        StartCoroutine(waitForSendAttack());
    }


    //Heavy Attack - low chance to hit does 25 damage.
    public void playerAttack2()
    {
        if (BattleManeger.Instance.whichturn != 1) return;
        AttackPower = (float) (powerAttack * 2);
        playerAnimator.SetTrigger("HeavyAttack");
        if (showDebug)
        {
            Debug.Log("HeavyAttack successed: " +
                      isAnimationRunning(playerAnimator.GetCurrentAnimatorStateInfo(0), "HeavyAttack"));
        }


        StartCoroutine(waitForSendAttack());
    }

    //Drinking Potion - Player is healed with full health.
    public void playerHeal()
    {
        if (BattleManeger.Instance.whichturn == 1)
        {
            if (player.playerHavePotions())
            {
                playerAnimator.SetTrigger("DrnkingPotion");
                playerDrink();
            }
            else
            {
                playerAnimator.SetTrigger("NoPotionToDrink");
                //Todo need to do like playerDrink but no drink only animation
            }
        }

        if (showDebug)
        {
            Debug.Log("Player has potion: " +
                      isAnimationRunning(playerAnimator.GetCurrentAnimatorStateInfo(0),
                          "DrinkPotion")); //assuming this line is reached before the animation ends.
        }
    }

    IEnumerator waitForSendAttack()
    {
        yield return new WaitForSeconds(1.5f);
        // soon will be replace with animation that event animation will call the EnemyAttack 
        SendAttack();
    }

    //this fonction for event trigger;
    public void SendAttack()
    {
        BattleManeger.Instance.AttackOnEnemy(AttackPower);
        BattleManeger.Instance.whichturn = 2;
    }

    public void playerDrink()
    {
        StartCoroutine(WaitforEndOfDrink());
        /*
        player.DrinkPotion();

        if (showDebug)
        {
            Debug.Log("Player Drank the potion");
        }
        */
    }

    IEnumerator WaitforEndOfDrink()
    {
        yield return new WaitForSeconds(1);
        player.DrinkPotion();
        playerPotions_Text.text = player.localPlayer.Potions.ToString();
        yield return new WaitForSeconds(5.4f);
        BattleManeger.Instance.whichturn = 2;
    }


    /************** Start of Guy's Functions **************/

    /* 
     * isAnimationRunning - gets an animatorState and checks if the current animation-clip is the clipName animation.
     * this function is mainly used to check if the player is in combat mode, thus either attack, running away or drinking a potion.
     */
    private bool isAnimationRunning(AnimatorStateInfo animator, String clipName)
    {
        if (showDebug)
        {
            Debug.Log("Animation: " + clipName + " is runing: " +
                      (animator.IsName(clipName) && animator.normalizedTime < 1.0f));
        }

        return (animator.IsName(clipName) && animator.normalizedTime < 1.0f);
    }

    /*
     * Checking if the Actor is in Idle animation.
     * If the actor is in Idle mode, then the battle is in Desicion Stage.
     * Otherwise, the battle is in Result Stage.
     */

    public void playerRun()
    {
        lc.FadeToLevel("WorldMap");     
        //need to add: if the player came from the "Tutorial" level, then a prompt will say "Can't run away", otherwise the player goes back to the map "WorldMap".
    }

    /* Checking if both Player and Monster are in Idle mode, otherwise they're fighting. */
    public bool isAnimationIdle()
    {
        return (isAnimationRunning(playerAnimator.GetCurrentAnimatorStateInfo(0), "Idle") 
                    && isAnimationRunning(enemyAnimator.GetCurrentAnimatorStateInfo(0),"Idle"));
    }

    //turning off and on the User Interface
    private void turnUIOff()
    {
        UI.SetActive(false);
    }

    private void turnUIOn()
    {
        UI.SetActive(true);
    }

    //turning off and on the MainCamera
    private void turnOffMainCamera()
    {
        //mainCamera.gameObject.SetActive(false);
        mainCamera.enabled = false;
    }

    private void turnOnMainCamera()
    {
        //mainCamera.gameObject.SetActive(true);
        mainCamera.enabled = true;
    }

    //turning off and on the CinematicCamera
    private void turnOffCinematicCamera()
    {
        //cinematicCamera.gameObject.SetActive(false);
        cinematicCamera.enabled = false;
    }

    private void turnOnCinematicCamera()
    {
        //cinematicCamera.gameObject.SetActive(true);
        cinematicCamera.enabled = true;
    }

    /*
     *  start/end - BattlePhase
     *  function to enter Battle-Phase.
     *  in Battle-Phase:     
     *      Turning the User-Interface Off, so the player can't try and send another commands.
     *      Turning On CinematicCamera - for Cinematic View of an action.
     *      Turning off the MainCamera - to reduce computation power.
     *  in Desicio-Phase:
     *      the oppisite process of Battle-Phase.
     */
    private void startBattlePhase()
    {
        turnUIOff();
        turnOnCinematicCamera();
        turnOffMainCamera();

        if (showDebug)
        {
            Debug.Log("Started Battle Phase");
        }
    }

    private void endBattlePhase()
    {
        turnUIOn();
        turnOnMainCamera();
        turnOffCinematicCamera();

        if (showDebug)
        {
            Debug.Log("Finished Battle Phase");
        }
    }

    /************** Send of Guy's Functions **************/
}
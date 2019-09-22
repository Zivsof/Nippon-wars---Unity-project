using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************** Deprecated, this Script is kept only for archive purpeses **************/

/*
 *The Battle Mode Controller is devided between the states:
 *  1. Decision Stage: the player has options which he takes actions from; Attack, Healing and running away.
 *  2. Computer calculates the action the player took and the results.
 *  3. Playing out the final results out to the player, in the form of animator controller.
 *  4. if the player dies, then they are prompt with Losing Menu, which they get to restart the game, go back to Main Menu screen or quit the game.
 *  5. if the player survives, then they return to the world map, with the monster encountered is gone.
 */
public class BattleModeControllerScript : MonoBehaviour
{
    //the battle manager that Ziv wrote
    public BattleManeger bm;

    /* a boolean flag to determin if the current state is a Decision-Stage or Result-Stage */
    private bool isDecisionStage = true;

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
    public Animator Player;
    public Animator Enemy;
    public Animator CameraAnimationController; //the "idle" cinematic camera moves constantly on its own, unless a certain animation is active.
    public GameObject UI; //the User Interface, mainly used to turn off and on the interface for the UI.
    
    public Camera mainCamera; //the main Camera is the main camera that shows the 'Default' battle field view.
    public Camera cinematicCamera; //The cinematic Camera always hovers on the battle field, capturing the battle in a cinematic shot.


    // Re-Setting all the options
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        checkIfDecisionTime(); //checking if the current stage is either Decision or Result.

        //if (isAnimationRunning(Player.GetCurrentAnimatorStateInfo(0), "Attack"))
        if (isDecisionStage) //if we're NOT in decision-Stage, then we're in Result-Stage.
        {
            //turnUIOff();
            startBattlePhase();
        }
        else
        {
            //turnUIOn();
            endBattlePhase();
        }
    }


    /*
     * Decision Phase:
     *  This phase gives the player time to chose their action.
     *  the main 4 actions are:
     *      1. Light Attack: high chance of successfully attacking, but only does a partial damage.
     *      2. Heavy Attack: low chance to successfully attacking, but does a lot of damage.
     *      3. Drink Potion: an action with a certain success, re-fills the player's health back to 100%.
     *      4. Run: a low chance to successfully running away from the battle, results with the enemy to disapear from the world and not getting the exp for it.
     *  
     *  Result Phase:
     *   Depanding on the action, the game calculates success chances.
     *   After deciding the results, the game cancels the User Interface so the player won't get the chance to give more input.
     *   By the results of the chances, the computer runs the animations of the Player, the Monster and the cinematic camera.
     *   after the animation is run down, the player's U.I is set back to enabled, thus letting the player see how much damage is done.
     *   after the Result Phase is done, the U.I is back on and the player is getting back into the Decision Phase.
     */




    private void checkIfDecisionTime()
    {
        if (isAnimationRunning(Player.GetCurrentAnimatorStateInfo(0), "LightAttack") 
                || isAnimationRunning(Player.GetCurrentAnimatorStateInfo(0), "HeavyAttack")
                    || isAnimationRunning(Player.GetCurrentAnimatorStateInfo(0), "TakingDamage")
                        || isAnimationRunning(Player.GetCurrentAnimatorStateInfo(0), "DrnkingPotion"))
        {
            isDecisionStage = true;
        }
        else isDecisionStage = false;

        if (showDebug)
        {
            Debug.Log(isDecisionStage);
        }
    }

    /* Decision Phase Functions */
    private void rollChance()
    {
        chance = UnityEngine.Random.Range(0, 10);
    }

    public void lightAttack()
    {
        rollChance();
        
        if(lightAttackChance + chance > lightAttackChance)
        {
            UI.gameObject.SetActive(false);
            Player.SetTrigger("LightAttack");
            bm.AttackOnEnemy(lightAttackDamage);
        }

        if (showDebug)
        {
            Debug.Log("Light Attack done.");
            Debug.Log("success chance is: " + (lightAttackChance + chance > lightAttackChance));
        }
    }


    public void heavyAttack()
    {
        rollChance();

        if (heavyAttackChance + chance >= 9)
        {
            bm.AttackOnEnemy(heavyAttackDamage);
            Player.SetTrigger("HeavyAttack");
        }

        if (showDebug)
        {
            Debug.Log("Heavy Attack done.");
            Debug.Log("success chance is: " + (heavyAttackChance + chance >= 9));
        }
    }
    /* Healing via using a Potion -  a potion has 100% to successed as an action, but the enemy might still try and attack after the heal. */
    public void drinkPotion()
    {
        if (heavyAttackChance + chance >= 9)
        {
            //bm.;
            Player.SetTrigger("DrnkingPotion");
        }

        if (showDebug)
        {
            Debug.Log("Drinking a potion is done.");
        }
    }


    public void runAway()
    {

    }

    //given a name and animator state, comparing if a spacific animation is running.
    private bool isAnimationRunning(AnimatorStateInfo animator, String clipName)
    {
        return (animator.IsName(clipName) && animator.normalizedTime < 1.0f);
    }

    public void endResultPhase()
    {
        if (showDebug)
        {
            Debug.Log("Entered endResultPhase");
        }
        endBattlePhase();
    }


    /* User Interface Control */

    /* Indecates that the battle Phase has Begun - 
 *          turning the UI off so the player can't add commands until the phase is over.
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
        turnOnMainCamera();
        turnUIOn();
        turnOffCinematicCamera();
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
        mainCamera.gameObject.SetActive(false);
    }
    private void turnOnMainCamera()
    {
        mainCamera.gameObject.SetActive(true);
    }
    //turning off and on the CinematicCamera
    private void turnOffCinematicCamera()
    {
        cinematicCamera.gameObject.SetActive(false);
    }
    private void turnOnCinematicCamera()
    {
        cinematicCamera.gameObject.SetActive(true);
    }

}

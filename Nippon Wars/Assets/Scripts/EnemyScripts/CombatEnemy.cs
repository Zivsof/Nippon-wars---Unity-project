using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CombatEnemy : MonoBehaviour
{
    public float theEnemyAttack;
    public EnemyState theEnemy;
    private int hitTimes = 0;

    public bool enemyDead = false;
    private Animator _enemyAnimator;
    private void Awake()
    {
        _enemyAnimator = GameObject.FindWithTag("Enemy").GetComponentInChildren<Animator>();
        if (SceneManager.GetActiveScene().name == "Testing 2")
        {
            this.GetComponent<NPCMovement>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Testing 2")
        {
            if (BattleManeger.Instance.whichturn == 2)
            {
                if (hitTimes == 0)
                {
                    // you can replace this function below with animation. 
                    StartCoroutine(waitForAttack());
                }
            }
        }
    }

    public void KillYourSelf()
    {
        enemyDead = true;
        Destroy(this.gameObject);
    }

    IEnumerator waitForAttack()
    {
        hitTimes++;
        yield return new WaitForSeconds(2);
        _enemyAnimator.SetTrigger("LightAttack");
        yield return new WaitForSeconds(2);
        // soon will be replace with animation that event animation will call the EnemyAttack 
        enemyAttack();

        hitTimes--;
    }

    //Function below can be used and event animation
    void enemyAttack()
    {
        BattleManeger.Instance.AttackOnPlayer();
        BattleManeger.Instance.whichturn = 1;
    }
}
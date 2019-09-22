using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyState theEnemy;
    public Image currentHealthbar;
    public Text ratioText;

    public float hitpoint;
    public float maxHitpoint;

    private void Awake()
    {
       
       
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Testing 2")
        {
            theEnemy = GameObject.FindWithTag("Enemy").GetComponent<EnemyState>();
            currentHealthbar = GameObject.Find("EnemyCurrentHP").GetComponent<Image>();
            ratioText = GameObject.Find("EnemyRatioText").GetComponent<Text>();
            
        }
        if (SceneManager.GetActiveScene().name == "Testing 2")
        {
            hitpoint = maxHitpoint = theEnemy.hp;
            UpdateHealthbar();
        }

    }

    private void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "Testing 2")
        {
            if (BattleManeger.Instance.PlayerWin)
            {
                hitpoint = 0;
                UpdateHealthbar();
            }
            hitpoint = theEnemy.hp;
            UpdateHealthbar();
        }
        
    }
    public void UpdateHealthbar()
    {
        float ratio = hitpoint / maxHitpoint;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0") + '%';
    }


    public void ReciveDamage(float damage)
    {
        Debug.Log(damage);
        float enemyHp = theEnemy.hp;
        if (enemyHp > damage)
        {
            hitpoint -= damage;
            theEnemy.hp -= damage;
        }
        else
        {
            hitpoint = 0;
            theEnemy.hp = 0;
            BattleManeger.Instance.PlayerWin = true;
        }
    }
}

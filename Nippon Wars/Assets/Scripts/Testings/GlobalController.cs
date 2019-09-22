using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

[Serializable]
public class enemy
{
    public bool IsDead;
    public int id;
}

[Serializable]
public class Item
{
    public bool IsUsed;
    public int id;
}

public class GlobalController : MonoBehaviour
{
    public PlayerStatistics savedPlayerDate = new PlayerStatistics();
    public static GlobalController Instance;
    public List<Item> items;
    public List<enemy> enemies1;

    [HideInInspector] public int enemyID;
    [HideInInspector] public bool battleModeActive;
    [HideInInspector] public bool playerDefeated;

    [HideInInspector] public int sceneToReturn;
    public Vector3 playerFWPosition;
    public Quaternion playerFWRotation;
    public int monstersRemain;

    void Awake()
    {
        Debug.Log("hi");
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SetIdEnemy();
        SetIdItem();
        GrabEnemies();
        GrabItems();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            Debug.Log("Destroying");
            DestroyEnemyOnLoad();
            sceneToReturn = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Testing")
        {
            sceneToReturn = 0;
        }
        else if (SceneManager.GetActiveScene().name == "WorldMap")
        {
            sceneToReturn = 2;
        }
    }

    private void Update()
    {
        if (battleModeActive && SceneManager.GetActiveScene().name != "Testing 2")
        {
            monstersRemain--;
            DestroyEnemyOnLoad();
            DestroyItemOnLoad();
            battleModeActive = false;
        }
    }

    private void DestroyEnemyOnLoad()
    {
        var enemieslevel = GameObject.FindGameObjectsWithTag("Enemy");
       

        foreach (var globEnemy in enemies1)
        {
            if (!globEnemy.IsDead) continue;
            foreach (var levelEnemy in enemieslevel)
            {
                if (levelEnemy.gameObject.GetComponent<EnemyState>().id.Equals(globEnemy.id))
                {
                    Destroy(levelEnemy.gameObject);
                }
            }
        }
    }

    private void DestroyItemOnLoad()
    {
        var itemslevel = GameObject.FindGameObjectsWithTag("Item");

        foreach (var globItem in items)
        {
            if (!globItem.IsUsed) continue;
            foreach (var levelItem in itemslevel)
            {
                if (levelItem.gameObject.GetComponent<ItemScript>().itemID.Equals(globItem.id))
                {
                    Destroy(levelItem);
                }
            }
        }
    }

    private void SetIdEnemy()
    {
        int newId = 0;
        foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            obj.GetComponent<EnemyState>().id = newId;
            newId++;
        }
    }

    private void GrabEnemies()
    {
        GameObject[] badGuys = GameObject.FindGameObjectsWithTag("Enemy");
        //int size = badGuys.Length;
        if (SceneManager.GetActiveScene().name == "WorldMap")
        {
            Debug.Log("hiiiii");
            monstersRemain = badGuys.Length;
        }

        foreach (var bad in badGuys)
        {
            enemies1.Add(new enemy()
            {
                id = bad.GetComponent<EnemyState>().id, IsDead = false
            });
        }
    }

    private void SetIdItem()
    {
        int newId = 0;
        foreach (var obj in GameObject.FindGameObjectsWithTag("Item"))
        {
            obj.GetComponent<ItemScript>().itemID = newId;
            newId++;
        }
    }

    private void GrabItems()
    {
        GameObject[] worldItems = GameObject.FindGameObjectsWithTag("Item");
        //int size = worldItems.Length;
        foreach (var bad in worldItems)
        {
            items.Add(new Item()
            {
                id = bad.GetComponent<ItemScript>().itemID, IsUsed = false
            });
        }
    }
}
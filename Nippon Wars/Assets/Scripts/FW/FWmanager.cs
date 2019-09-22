using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWmanager : MonoBehaviour
{
    public static FWmanager Instance;
    [Header("Dialog setting")]
    public bool enemyDialog;
    public bool DialogStart;
    private GameObject levelSwaper;
    private string SceneName;
    private GameObject thePlayer;
    [Header("Player Settings")]
    public GameObject PlayerStats;

    public CharacterMovment PlayerMovment;
    public GameObject PlayerInteraction;
    [Header("Scene Fader and Pause Menu ")]
    public GameObject SceneFader;
    public GameObject puseMenu;

    public string BattleSceneName;
    // Start is called before the first frame update

    private void Awake()
    {
        thePlayer = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        SceneFader.SetActive(true);
        levelSwaper = GameObject.FindWithTag("LevelChanger");
        SetPlayerPosition();
        PlayerMovment = thePlayer.GetComponent<CharacterMovment>();
        PlayerInteraction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        RenderSettings.skybox.SetFloat("_Rotation",Time.time * 1);
        if (enemyDialog)
        {
           LoadOtherScene(SceneName);
        }

        if (DialogStart)
        {
            thePlayer.GetComponent<Animator>().SetFloat("Forward",0);
            PlayerMovment.enabled = false;
            thePlayer.GetComponent<NewPlayerController>().enabled = false;
            PlayerInteraction.SetActive(false);
        }
        else
        {
            PlayerMovment.enabled = true;
            thePlayer.GetComponent<NewPlayerController>().enabled = true;
        }

        //PlayerStats.SetActive(!DialogStart);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                PlayerStats.SetActive(true);
                puseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                PlayerStats.SetActive(false);
                puseMenu.SetActive(false);
            }
        }
    }

    public void GoToMainMenu()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            puseMenu.SetActive(false);
            PlayerStats.SetActive(false);
            ChangeToLevel("Main Menu");
            //Destroy(GlobalController.Instance);
            levelSwaper.GetComponent<LevelChanger>().FadeToLevel(SceneName);
            
        }
    }

    public void KeepPlay()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            PlayerStats.SetActive(false);
            puseMenu.SetActive(false);
        }
    }

    public void ChangeToBattleMode()
    {
        SceneName = BattleSceneName;
    }

    public void GetPlayerPosition()
    {
        GlobalController.Instance.playerFWPosition = thePlayer.transform.position;
        GlobalController.Instance.playerFWRotation = thePlayer.transform.rotation;
    }

    private void SetPlayerPosition()
    {
        thePlayer.transform.position = GlobalController.Instance.playerFWPosition;
        thePlayer.transform.rotation = GlobalController.Instance.playerFWRotation;
    }

    public void ChangeToLevel(string levelName)
    {
        SceneName = levelName;
    }

    public void GetEnemyId(GameObject tag)
    {
        foreach (var enemy in GlobalController.Instance.enemies1)
        {
            if (enemy.IsDead) continue;

            if (!tag.GetComponent<EnemyState>().id.Equals(enemy.id)) continue;
            enemy.IsDead = true;
            GetEnemyName(tag.GetComponent<EnemyState>().ememyType);
            break;
        }
    }

    private void GetEnemyName(String st)
    {
        switch (st)
        {
            case "Enemy1":
                GlobalController.Instance.enemyID = 1;
                break;
            case "Enemy2":
                GlobalController.Instance.enemyID = 2;
                break;
            case "Enemy3":
                GlobalController.Instance.enemyID = 3;
                break;
             default:
                 Debug.LogError(st + " not in FWmanager->GetEnemeyName's Dictionary");
                 break;
        }
        
    }

    public void  LoadOtherScene(String st)
    {
        SceneName = st;
        levelSwaper.GetComponent<LevelChanger>().FadeToLevel(SceneName);
    }
    
    public void GetItemId(GameObject tag)
    {
        GameObject itemCollision = tag;
        foreach (var item in GlobalController.Instance.items)
        {
            if (item.IsUsed) continue;
            if (!itemCollision.GetComponent<ItemScript>().itemID.Equals(item.id)) continue;
            item.IsUsed = true;
            break;
        }
    }
    
}
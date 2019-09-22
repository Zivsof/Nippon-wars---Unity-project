using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (GlobalController.Instance != null && SceneManager.GetActiveScene().name=="Main Menu")
        {
            Destroy(GlobalController.Instance);
        }
    }

    public void PlayGame()
    {
        Debug.Log("Tutorial");
        SceneManager.LoadScene("Tutorial");
    }
    public void SkipTutorial()
    {
        if (SceneManager.GetActiveScene().name =="VictoryScene")
        {
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            SceneManager.LoadScene("WorldMap");
        }
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}

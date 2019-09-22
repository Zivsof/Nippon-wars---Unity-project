using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapController : MonoBehaviour
{
    public GameObject victoryPanel;
    private bool gameEnd = true;
    // Start is called before the first frame update
    void Start()
    {
        var enemieslevel = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalController.Instance.monstersRemain ==0&& gameEnd)
        {
            if (gameEnd)
            {
                StartVictory();
            }
            
        }
    }

    void StartVictory()
    {
        gameEnd = false;
        victoryPanel.SetActive(true);
        StartCoroutine(LeavingGame());
    }

    IEnumerator LeavingGame()
    {
        yield return new WaitForSeconds(3);
        victoryPanel.SetActive(false);
        FWmanager.Instance.LoadOtherScene("VictoryScene");
    }
}

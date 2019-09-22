using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionEnemy : MonoBehaviour
{
    private Collider myCol;
    
    private void Awake()
    {
        myCol = this.gameObject.GetComponent<Collider>();
        myCol.enabled = !myCol.enabled;
    }

    public void Start()
    {
        if (checkColliderScene())
        {
            StartCoroutine(waitColision());
        }
    }

    IEnumerator waitColision()
    {
        yield return new WaitForSeconds(1);
        myCol.enabled = !myCol.enabled;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (checkColliderScene())
        {
            if (collision.transform.tag == "Player")
            {
                if (this.gameObject.GetComponents<DialogueTrigger>() != null)
                {
                    this.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else
                {
                    Debug.LogError("You forgot to put on me DialogueTrigger Name "+this.name);
                }
                FWmanager.Instance.ChangeToBattleMode();
                
            }
        }
    }
    
    bool checkColliderScene()
    {
        return (SceneManager.GetActiveScene().name != "Testing 2");
        //return (SceneManager.GetActiveScene().name == "Testing" || SceneManager.GetActiveScene().name == "Tutorial"|| SceneManager.GetActiveScene().name == "WorldMap");
    }
}
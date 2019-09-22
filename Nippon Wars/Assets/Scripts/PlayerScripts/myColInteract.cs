using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class myColInteract : MonoBehaviour
{
    private BoxCollider myCol;

    private void Awake()
    {
        myCol = GetComponent<BoxCollider>();
        myCol.enabled = !myCol.enabled;
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "Testing")
        {
            StartCoroutine(waitColision());
        }
    }

    IEnumerator waitColision()
    {
        yield return new WaitForSeconds(1);
        myCol.enabled = !myCol.enabled;
    }
}
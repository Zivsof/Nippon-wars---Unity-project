using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movingScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            print("space key was pressed");
            SceneManager.LoadScene("Testing 2");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            print("space key was pressed");
            SceneManager.LoadScene("Testing");
        }
    }
}

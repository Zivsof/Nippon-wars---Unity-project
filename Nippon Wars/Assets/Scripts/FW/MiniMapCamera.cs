using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniMapCamera : MonoBehaviour
{
    public Transform thePlayer;

    private void LateUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Testing 2"||SceneManager.GetActiveScene().name != "Testing")
        {
            Vector3 newPosition = thePlayer.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
        }
       
    }
}

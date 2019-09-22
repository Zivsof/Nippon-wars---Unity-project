using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#pragma warning disable 
public class PlayerStatesFW : PlayerState
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        //transform.position = localPlayer.position;
    }

    // Update is called once per frame
    void Update()
    {
        //localPlayer.position = transform.position;
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{
    public Transform lookAtMe;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(lookAtMe);
    }
}

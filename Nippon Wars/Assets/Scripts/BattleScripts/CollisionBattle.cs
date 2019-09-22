using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionBattle : MonoBehaviour
{
    public string FWname;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            FWmanager.Instance.LoadOtherScene("WorldMap");
        }
    }
}

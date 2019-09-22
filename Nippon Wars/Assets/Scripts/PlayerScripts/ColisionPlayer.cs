using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionPlayer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            FWmanager.Instance.GetEnemyId(collision.gameObject);
        }
    }
    
}

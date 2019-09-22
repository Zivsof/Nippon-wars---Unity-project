using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockShatter : MonoBehaviour
{
    public GameObject rockShattered;
    
    public void Explosion()
    {
        Instantiate(rockShattered, transform.position, transform.rotation);
        GetComponent<Rigidbody>().AddExplosionForce(100000,transform.position,5);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap : MonoBehaviour
{
    public bool showDebug = true;
    //public float TargetDistance;
    public Animator arrow;
    public float DistanceToShoot = 10;
    public GameObject exPartical;
    private bool isPlayerHere;
    void Update()
    {
        RaycastHit Hit;
        if (Physics.Raycast(transform.position + (Vector3.up * 2), transform.TransformDirection(Vector3.forward), out Hit)
                && Hit.distance <= DistanceToShoot
                    && Hit.collider.tag.Equals("Player"))
        {
            if (showDebug)
            {
                Debug.DrawRay(transform.position + (Vector3.up * 2), (Vector3.forward * (Hit.distance)), Color.green);
                Debug.Log("Hit distance: " + Hit.distance);
                //TargetDistance = Hit.distance;
            }
            FireArrow();
        }
    }

    private void FireArrow()
    {
        Instantiate(exPartical, transform.position, transform.rotation);
        arrow.SetTrigger("Trigger");
        this.enabled = false;
    }

}

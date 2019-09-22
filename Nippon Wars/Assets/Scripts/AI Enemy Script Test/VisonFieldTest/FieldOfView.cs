using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)] public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    private RaycastHit playerHit;

    //AIIncludeMoveC
    
    [HideInInspector] public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2);
    }


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Debug.Log("Staring Cortian");
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        /*
        //todo change back when need ( AIIncludeMoveC>NPCSimplePatrol)
        if (this.gameObject.GetComponent<AIIncludeMoveC>())
        {
            this.gameObject.GetComponent<AIIncludeMoveC>()._targetPlayer = null;
        }
        else if (this.gameObject.GetComponent<NPCSimplePatrol>())
        {
            this.gameObject.GetComponent<NPCSimplePatrol>()._targetPlayer = null;
        }
        */
        
        
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    if (Physics.Raycast(transform.position, dirToTarget, out playerHit))
                    {
                        if (playerHit.collider.gameObject.CompareTag("Player"))
                        {
                            
                            GameObject targetPlayer = playerHit.collider.gameObject;
                            if (this.gameObject.GetComponent<AIIncludeMoveC>())
                            {
                                this.gameObject.GetComponent<AIIncludeMoveC>().isplayerInBound = true;
                                this.gameObject.GetComponent<AIIncludeMoveC>()._targetPlayer = targetPlayer;
                            }
                            else if (this.gameObject.GetComponent<NPCSimplePatrol>())
                            {
                                this.gameObject.GetComponent<NPCSimplePatrol>().isplayerInBound = true;
                                this.gameObject.GetComponent<NPCSimplePatrol>()._targetPlayer = targetPlayer;
                            }
                        }
                    }

                    visibleTargets.Add(target);
                }
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
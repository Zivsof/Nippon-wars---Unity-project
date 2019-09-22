using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NPCMovement : MonoBehaviour
{
    public float distanceToStartChace = 15.0f;
    public Transform defaultDestination;
    [SerializeField] Transform destination;

    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponentInChildren<Animator>().SetBool("Walking", true);

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (defaultDestination == null)
        {
            defaultDestination = transform;
        }

        if (navMeshAgent == null)
        {
            Debug.Log("The nav mesh agent component is missing: " + gameObject.name);
        }
        else
        {
            setDestination();
        }

        if(!SceneManager.GetActiveScene().name.Equals("WorldMap"))
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (destination!=null && Vector3.Distance(destination.position, transform.position) <= distanceToStartChace)
        {
            setDestination();
        }
        else
        {
            navMeshAgent.SetDestination(defaultDestination.position);
        }

        if (SceneManager.GetActiveScene().name.Equals("WorldMap"))
        {
            if (navMeshAgent.destination.Equals(defaultDestination.position)
               || navMeshAgent.destination.Equals(gameObject.transform.position))
            {
                gameObject.GetComponentInChildren<Animator>().SetBool("Walking", false);
            }
            else
            {
                gameObject.GetComponentInChildren<Animator>().SetBool("Walking", true);
            }
        }
        else
        {
            gameObject.GetComponentInChildren<Animator>().SetBool("Walking", false);
        }
 
    }

    private void setDestination()
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }
}
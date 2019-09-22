using System.Collections;
using UnityEngine;

public class RailMover : MonoBehaviour
{
    public Rails rails;
    public Transform lookAt;
    public bool smoothMove = true;
    public float moveSpeed = 5.0f;

    public Vector3 distanceToKeep;

    private Transform thisTransform;
    private Vector3 lastPosition;
    private void Start()
    {
        thisTransform = transform;
        lastPosition = thisTransform.position;
        if (distanceToKeep == null)
        {
            distanceToKeep = Vector3.zero;
        }
    }
    private void Update()
    {
        if (smoothMove)
        {
            lastPosition = Vector3.Lerp(lastPosition, rails.ProjectPositionOnRail(lookAt.position), moveSpeed * Time.deltaTime);
            thisTransform.position = lastPosition - distanceToKeep;
        }
        else
        {
            thisTransform.position = rails.ProjectPositionOnRail(lookAt.position) - distanceToKeep;
        }

        thisTransform.LookAt(lookAt.position);

        //thisTransform.position = rails.ProjectPositionOnRail(lookAt.position);
    }

}

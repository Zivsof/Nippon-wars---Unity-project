using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#pragma warning disable
public class AIIncludeMoveC : MonoBehaviour
{
    //Replica of NPCSimplePatrol
    [Header("The Patrol")] [SerializeField]
    private bool _patrolWaiting;

    [SerializeField] private float _totalWaitTime = 3f;
    [SerializeField] private List<Waypoint> _patrolPoints;

    private NavMeshAgent _navMeshAgent;
    private int _currentPatrolIndex;
    private bool _travelling;
    private bool _waiting;
    private bool _patrolForward;
    private float _waitTimer;
    public CharacterMovment character { get; private set; }
    [Header("The Agro")] public bool _attackMode;
    public GameObject _targetPlayer;
    private AiTriggerPatrol _trigg_Patrol;
    public float _attackDistance;

    public bool isplayerInBound;

    private void OnEnable()
    {
        _trigg_Patrol = this.GetComponentInChildren<AiTriggerPatrol>();
        _trigg_Patrol.agroRad = GetComponent<FieldOfView>().viewRadius;
    }

    void Start()
    {
        character = this.GetComponent<CharacterMovment>();

        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.LogError("the nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            if (_patrolPoints != null && _patrolPoints.Count >= 2)
            {
                _currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient patrol points for basic patrolling behaviour");
            }
        }
    }

    private void Update()
    {
        if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
            character.Move(_navMeshAgent.desiredVelocity, false, false);
        else
            character.Move(Vector3.zero, false, false);


        if (_targetPlayer != null && isplayerInBound)
        {
            Debug.Log("Dear Player: Do you know the way?");
            CheckDestinationPlayer();
        }
        else
        {
            if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
            {
                Debug.Log("Paroling");
                _travelling = false;
                if (_patrolWaiting)
                {
                    _waiting = true;
                    _waitTimer = 0;
                }
                else
                {
                    ChangePatrolPoint();
                    SetDestination();
                }
            }

            if (_waiting)
            {
                character.Move(Vector3.zero, false, false);
                _waitTimer += Time.deltaTime;
                if (_waitTimer >= _totalWaitTime)
                {
                    _waiting = false;

                    ChangePatrolPoint();
                    SetDestination();
                }
            }
        }
    }

    private void ChangePatrolPoint()
    {
        _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
    }


    private void SetDestination()
    {
        if (_patrolPoints == null) return;
        Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
        _navMeshAgent.SetDestination(targetVector);
        _travelling = true;
    }

    public void CheckDestinationPlayer()
    {
        float dist = Vector3.Distance(transform.position, _targetPlayer.transform.position);
        if (dist <= _attackDistance)
        {
            _attackMode = true;
            _navMeshAgent.isStopped = true;
        }
        else
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = _targetPlayer.transform.position;
            _attackMode = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Enemy"))
        {
            _navMeshAgent.speed = 0;
            character.Move(Vector3.zero, false, false);
        }
    }
}
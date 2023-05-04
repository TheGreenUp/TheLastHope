
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BlindAI : MonoBehaviour
{
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderDistance = 5f;
    [SerializeField] private float movementSpeed = 5f;

    private NavMeshAgent navMeshAgent;
    private Vector3 destination;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetRandomDestination();
    }

    private void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        Vector3 localTarget = transform.position + Random.insideUnitSphere * wanderRadius;
        NavMesh.SamplePosition(localTarget, out NavMeshHit hit, wanderDistance, NavMesh.AllAreas);
        destination = hit.position;
        navMeshAgent.SetDestination(destination);
        navMeshAgent.speed = movementSpeed;
    }
}






using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{
    public float detectionRadius = 10f;
    public LayerMask detectionLayer;
    public float moveSpeed = 5f;
    public Transform target;

    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderDistance = 5f;

    private NavMeshAgent navMeshAgent;

    private Vector3 destination;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                AudioSource audioSource = collider.GetComponent<AudioSource>();
                if (audioSource != null && audioSource.isPlaying)
                {
                    if (target.CompareTag("Player") && target.localPosition.y <= 0.05)
                    {
                        Debug.LogError("ÊÓÄÀ ÎÍ ÄÅËÑß ÍÀÓÕÉ");
                        return;
                    }
                    goFuck();
                }

            }
        }
       
    }

    private void SetRandomDestination()
    {
        Debug.Log("Äýñòèíåéøîí àííîóí");
        Vector3 localTarget = transform.position + Random.insideUnitSphere * wanderRadius;
        NavMesh.SamplePosition(localTarget, out NavMeshHit hit, wanderDistance, NavMesh.AllAreas);
        destination = hit.position;
        Debug.DrawLine(transform.position, destination, Color.green, 2f);
        navMeshAgent.SetDestination(destination);
        navMeshAgent.speed = moveSpeed;
    }

    private void goFuck()
    {
        Debug.LogWarning("ÈÄÓ ÁÈÒÜ ÅÁÀËÎ " + target.name);
        navMeshAgent.SetDestination(target.position);
    }
    void OnDrawGizmosSelected()
    {
        // Îòðèñîâêà ñôåðû ïîêðûòèÿ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}

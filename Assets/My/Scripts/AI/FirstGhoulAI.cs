using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FirstGhoulAI : MonoBehaviour
{
    public GameObject targetToFollow;

    private NavMeshAgent monster;
    private Animator animator;
    void Start()
    {
        monster = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        if (true)
        {
            FollowPlayer();
        }
    }
    void FollowPlayer()
    {
        FaceTarget();
        if (Vector3.Distance(monster.transform.position, targetToFollow.transform.position) > 1f)
        {
        animator.SetBool("isCrawlingFast", true);    
        monster.SetDestination(targetToFollow.transform.position);
        }
        else
        {
            animator.SetBool("isCrawlingFast", false);
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (targetToFollow.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator animator;

    float vertical;
    float horizontal;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (vertical == 0f)
        {
            animator.SetBool("Run",false);
        }
        if (vertical >= 0f)
        {
            animator.SetBool("Run",true);
        }
    }
}

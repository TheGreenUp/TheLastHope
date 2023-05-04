using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    public GameObject ball;
    
    private void OnTriggerEnter(Collider other)
    {
        ball.SetActive(true);
    }
}

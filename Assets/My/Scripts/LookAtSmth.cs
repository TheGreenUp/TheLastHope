using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtSmth : MonoBehaviour
{
    // Update is called once per frame
    public GameObject player;
    void Update()
    {
        transform.LookAt(player.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterScript : MonoBehaviour
{

    public GameObject tol4ok;
    private void OnTriggerEnter(Collider other)
    {
        tol4ok.GetComponent<AudioSource>().Play();
    }
}

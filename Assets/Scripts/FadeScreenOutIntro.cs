using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreenOutIntro : MonoBehaviour
{
    public GameObject FadeScreen;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObject.GetComponent<Animation>().Play("FadeScreenOut");
        }
    }
}

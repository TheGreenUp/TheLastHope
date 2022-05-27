using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CampSoundActivator : MonoBehaviour
{
    public GameObject player;
    public AudioSource sound;
    public GameObject campfire;
    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        campfire.GetComponent<AudioSource>().enabled = false;
        
    }
    private void OnTriggerExit(Collider other)
    {
        campfire.GetComponent<AudioSource>().enabled = false;
    }


}

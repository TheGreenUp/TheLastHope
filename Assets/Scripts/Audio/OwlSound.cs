using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlSound : MonoBehaviour
{
    public AudioSource sound;
    public float maxRepeatTime;
    public float minRepeatTime;

    public AudioClip[] owlSounds = default;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startOwlSounds());

    }

    IEnumerator startOwlSounds()
    {
        while (true)
        {
            sound.PlayOneShot(owlSounds[Random.Range(0, owlSounds.Length - 1)]);    
     

           // sound.Play();
            yield return new WaitForSeconds(Random.Range(minRepeatTime, maxRepeatTime));
        }
    }
}

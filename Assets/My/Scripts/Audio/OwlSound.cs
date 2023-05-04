using System.Collections;
using UnityEngine;

public class OwlSound : MonoBehaviour
{
    public AudioSource sound;
    public float maxRepeatTime;
    public float minRepeatTime;

    public AudioClip[] owlSounds = default;
    void Start()
    {
        StartCoroutine(startOwlSounds());

    }
    IEnumerator startOwlSounds()
    {
        while (true)
        {
            sound.PlayOneShot(owlSounds[Random.Range(0, owlSounds.Length - 1)]);    
            yield return new WaitForSeconds(Random.Range(minRepeatTime, maxRepeatTime));
        }
    }
}

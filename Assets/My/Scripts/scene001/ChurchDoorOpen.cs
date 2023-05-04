using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchDoorOpen : MonoBehaviour
{
    public GameObject gameManager;

    public GameObject leftDoor;
    public GameObject rightDoor;

    public int playerNumberOfNotes;
    public int neededNumberOfNotes;

    public AudioSource audioSource;
    public AudioClip churchMusic;

    private bool isActive = true;


    private void OnTriggerEnter(Collider other)
    {
        
            playerNumberOfNotes = gameManager.GetComponent<Collectables>().getNumberOfNotes();
            if((playerNumberOfNotes >= neededNumberOfNotes) && isActive)
            {
                audioSource.PlayOneShot(churchMusic);
                leftDoor.GetComponent<Animation>().Play("LeftChurchDoorAnim");
                leftDoor.GetComponent<AudioSource>().Play();
                rightDoor.GetComponent<Animation>().Play("RightChurchDoorAnim");
                rightDoor.GetComponent<AudioSource>().Play();
                isActive = false;
                
        }
    }
}

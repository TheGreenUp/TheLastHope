// Coded by Developer Jake -- https://www.youtube.com/developerjake
// Follow the Backrooms Game Lab (Part 7) to understand what this is for

using UnityEngine;

public class KeyDoor : MonoBehaviour // This script should be on the Locked Door Trigger
{
    [Header("Attributes")]
   
    [Tooltip("The name of the key that is required.")] public string keyName = "";

    [Header("References")]

    public GameObject CursorHover; // The hover cursor that should show when the player is looking at the door

    public Animation Door;


    private KeyManager km;

    public AudioSource audioSource;

    public AudioClip lockedDoorSound;
    public AudioClip openDoor;
    public AudioClip closeDoor;

    private bool isUnlocked = false;

    private void Start()
    {
        km = FindObjectOfType<KeyManager>(); // Assign
    }

    private void OnMouseOver() // Activates when the player looks at the door
    {

        if ( PlayerCasting.DistanceFromTarget <= 4 ) // If the player IS close enough to the door..
        {

            CursorHover.SetActive(true);



            if (Input.GetKeyDown(KeyCode.E)) // If the player presses E..
            {

                foreach (string key in km.keysInInventory) // Check to see if the player has key
                {
                    if (key.Trim().ToLower() == keyName.Trim().ToLower())
                    { 
                        GetComponent<BoxCollider>().enabled = false; // Turns off the player's ability to open the door again even though it's already open

                        Door.Play(); // Play the door open animation

                        audioSource.PlayOneShot(openDoor); // Play the door open sound

                        km.keysInInventory.Remove(key); // Removes the key from the inventory

                        isUnlocked = true;

                    }
                }

                if (!isUnlocked)
                {
                    
                    audioSource.PlayOneShot(lockedDoorSound);  
                }
            }


        }

        else // If the player is NOT close enough to the door
        {

            CursorHover.SetActive(false);

        }
    }



    private void OnMouseExit() // Activates when the player looks away from the door
    {

        CursorHover.SetActive(false);

    }
}

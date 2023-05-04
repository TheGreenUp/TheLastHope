 // Coded by Developer Jake -- https://www.youtube.com/developerjake
// Follow the Backrooms Game Lab (Part 7) to understand what this is for

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [Tooltip("The name of the key. This corresponds with the key on the door")] public string keyName;

    public GameObject HoverIcon;

    [Header("(Optional)")]
    [Tooltip("(Optional.)")] public AudioClip CollectAudio;

    public void OnMouseOver()
    {
        if (PlayerCasting.DistanceFromTarget <= 5) // Is the player in range?
        {
            HoverIcon.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {  // These events happen when the player is looking at the key, and is in range, and presses E.

                if (CollectAudio != null) FindObjectOfType<KeyManager>().GetComponent<AudioSource>().PlayOneShot(CollectAudio); // (Optional.)

                FindObjectOfType<KeyManager>().keysInInventory.Add(keyName);

                HoverIcon.SetActive(false);

                Destroy(this.gameObject);
            }
        }
        else HoverIcon.SetActive(false);
    }

    public void OnMouseExit()
    {
        HoverIcon.SetActive(false);
    }
}

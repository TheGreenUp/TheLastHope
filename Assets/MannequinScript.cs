using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinScript : MonoBehaviour
{
    public Transform playerTransform;
    public float lookTime = 3f;
    public float teleportDelay = 2f;
    public AudioSource audioSource;

    private bool isLookingAtMannequin = false;
    private float lookStartTime;
    private Renderer render;



    private void Start()
    {
        render = GetComponent<Renderer>(); 
    }
    private void Update()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, playerTransform.position, Color.red);

        if (Physics.Linecast(transform.position, playerTransform.position, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                if (!isLookingAtMannequin)
                {
                    isLookingAtMannequin = true;
                    lookStartTime = Time.time;
                }

                if (Time.time - lookStartTime >= lookTime)
                {
                    StartCoroutine(TeleportMannequin());
                }
            }
            else
            {
                isLookingAtMannequin = false;
            }
        }
    }

    private IEnumerator TeleportMannequin()
    {
        yield return new WaitForSeconds(teleportDelay);

        if (isLookingAtMannequin)
        {
            while (render.isVisible)
            {
                yield return null;
            }

            audioSource.Play();
            transform.position = new Vector3(transform.position.x, transform.position.y, 19);
        }
    }
}





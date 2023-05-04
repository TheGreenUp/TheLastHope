using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backroomsDoorClose : MonoBehaviour
{
    public GameObject door;
    public GameObject lightning;
    public GameObject lightning1;
    public AudioSource zvuk;
    public AudioClip closedDoorSound;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            roomStuff();
            Invoke(nameof(ChangeLevel), 4f);

        }
    }
    private void roomStuff()
    {
        door.GetComponent<AudioSource>().PlayOneShot(closedDoorSound);
        door.GetComponent<Animation>().Play("Door_Close");
        StartCoroutine(WaitAndDoSomething(2f));
      
    }
    private void ChangeLevel()
    {
        SceneManager.LoadScene(4);
    }
    public IEnumerator WaitAndDoSomething(float waitTime)
    {
    
        yield return new WaitForSeconds(waitTime);
        zvuk.Play();
        lightning.SetActive(false);
        zvuk.Play();
        lightning1.SetActive(false);
        this.gameObject.SetActive(false);

    }
}

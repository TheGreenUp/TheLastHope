using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CutSceneOnTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject cutsceneCamera;
    public GameObject fade;

    public AudioSource evilPhrase;
    public GameObject crosshair;

    public AudioClip[] audioClips;

    public int cutsceneLength;
    void OnTriggerEnter(Collider other)
    {
     if (other.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            crosshair.SetActive(false);
            cutsceneCamera.transform.position = other.transform.position;
            cutsceneCamera.SetActive(true);
            player.SetActive(false);
            StartCoroutine(FinishCutscene());    
        }   
    }

    IEnumerator FinishCutscene()   
    {
        fade.SetActive(true);
        evilPhrase.PlayOneShot(audioClips[1]);
        yield return new WaitForSeconds(0.3f);
        evilPhrase.PlayOneShot(audioClips[0]);
        yield return new WaitForSeconds(2f);
        evilPhrase.PlayOneShot(audioClips[2]);
        yield return new WaitForSeconds(cutsceneLength);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}

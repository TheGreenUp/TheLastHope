using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    public int introtime;
    public GameObject fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(LoadNextLevel());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            AudioListener.pause = true;
            Time.timeScale = 0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Update is called once per frame

    IEnumerator LoadNextLevel()
    {

        yield return new WaitForSeconds(introtime-4);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

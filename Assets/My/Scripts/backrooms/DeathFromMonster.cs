using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathFromMonster : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Mimic")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

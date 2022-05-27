using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartMission : MonoBehaviour
{
    public TMP_Text text;
    public AudioSource audioSource;
    public MissionUpdSO mission;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(updateMissionText(mission.missionSound.length));
        }
    }
    IEnumerator updateMissionText(float delay)
    {
        text.text = mission.text;
        audioSource.PlayOneShot(mission.missionSound);
        this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(delay-2f);
        text.text = "";
    }


}

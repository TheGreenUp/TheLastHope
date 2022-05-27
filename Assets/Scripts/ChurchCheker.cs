using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchCheker : MonoBehaviour
{
    public GameObject triggerForMissionUpdate;
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<Collectables>().numberOfNotes >= 3)
        {
            triggerForMissionUpdate.SetActive(false);
            triggerForMissionUpdate.GetComponent<BoxCollider>().isTrigger = false;
            triggerForMissionUpdate.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}

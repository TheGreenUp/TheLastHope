
using UnityEngine;

public class TriggerForSubtitle : MonoBehaviour
{
    public AudioObject clipToPlay;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { 
            Vocals.instance.Say(clipToPlay);
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

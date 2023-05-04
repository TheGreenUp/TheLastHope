using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{
    public GameObject gameManager;
    public override void OnFocus()
    {
        print("LOOKING AT " + gameObject.name);
    }

    public override void OnInteract()
    {
        print("INTERACTED WITH " + gameObject.name);
        gameManager.GetComponent<Collectables>().addNote();
        Destroy(gameObject);
    }
    public override void OnInteract(GameObject player)
    {
        print("INTERACTED WITH " + gameObject.name);
        Destroy(gameObject);
    }

    public override void OnLoseFocus()
    {
        print("STOPPED LOOKING AT " + gameObject.name);
    }


}

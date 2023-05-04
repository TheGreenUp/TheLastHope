using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class ClosedDoorOpen : MonoBehaviour
{
    public float theDistance;//расстояние до объекта, берем из рейкастинга в 19 строчке

    public GameObject actionKey;//[ e ]
    public GameObject actionText;//"Открыть дверь"
    public GameObject door;//сама дверь, что открывается
    public GameObject doorTrigger;//триггер на двери
    public GameObject extraCross;//Дополнительный ободок у прицела
    public TMP_Text subtitle;//Дополнительный ободок у прицела


    void Update()
    {
        theDistance = PlayerCasting.DistanceFromTarget;
    }
    void OnMouseOver()
    {
        if (theDistance < 1.2 && doorTrigger.GetComponent<BoxCollider>().isTrigger == true) //когда просто дистанция меньше (игрок смотрит на дверь)
        {
            actionKey.SetActive(true);
            actionText.SetActive(true);
            extraCross.SetActive(true);
        }
        if (Input.GetButtonDown("Action"))//когда нажали кнопку (е)
        {
            if (theDistance < 1.2 && doorTrigger.GetComponent<BoxCollider>().isTrigger == true)//когда дверь закрыта
            {
                actionKey.SetActive(false);
                actionText.SetActive(false);
                doorTrigger.SetActive(false);
                doorTrigger.GetComponent<BoxCollider>().isTrigger = false;
                StartCoroutine(theSequence());
            }
        }
    }
     void OnMouseExit()//увели мышку с триггера
    {
        actionKey.SetActive(false);
        actionText.SetActive(false);
        extraCross.SetActive(false);
    }

    IEnumerator theSequence()
    {
        subtitle.text = "Хм, заперто, может соседняя будет открыта.";
        yield return new WaitForSeconds(3f);
        subtitle.text = " ";
    }
}

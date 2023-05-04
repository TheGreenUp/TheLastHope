using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class BackroomsDoor : MonoBehaviour
{
    public float theDistance;//расстояние до объекта, берем из рейкастинга в 19 строчке

    public GameObject actionKey;//[ e ]
    public GameObject actionText;//"Открыть дверь"
    public GameObject door;//сама дверь, что открывается
    public GameObject extraCross;//Дополнительный ободок у прицела


    void Update()
    {
        theDistance = PlayerCasting.DistanceFromTarget;
    }
    void OnMouseOver()
    {
        if (theDistance < 1.2 && this.GetComponent<BoxCollider>().isTrigger == true) //когда просто дистанция меньше (игрок смотрит на дверь)
        {
            actionKey.SetActive(true);
            actionText.SetActive(true);
            extraCross.SetActive(true);
        }
        if (Input.GetButtonDown("Action"))//когда нажали кнопку (е)
        {
            if (theDistance < 1.2 &&
                this.GetComponent<BoxCollider>().isTrigger == true
                && GameObject.FindGameObjectWithTag("GameManager").
                GetComponent<Collectables>().getNumberOfNotes() > 0) ;//когда дверь закрыта
            {
                actionKey.SetActive(false);
                actionText.SetActive(false);
                this.GetComponent<BoxCollider>().gameObject.SetActive(false);
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
    void OnMouseExit()//увели мышку с триггера
    {
        actionKey.SetActive(false);
        actionText.SetActive(false);
        extraCross.SetActive(false);
    }

}

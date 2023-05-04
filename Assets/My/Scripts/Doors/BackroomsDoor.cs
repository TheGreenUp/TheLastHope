using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class BackroomsDoor : MonoBehaviour
{
    public float theDistance;//���������� �� �������, ����� �� ����������� � 19 �������

    public GameObject actionKey;//[ e ]
    public GameObject actionText;//"������� �����"
    public GameObject door;//���� �����, ��� �����������
    public GameObject extraCross;//�������������� ������ � �������


    void Update()
    {
        theDistance = PlayerCasting.DistanceFromTarget;
    }
    void OnMouseOver()
    {
        if (theDistance < 1.2 && this.GetComponent<BoxCollider>().isTrigger == true) //����� ������ ��������� ������ (����� ������� �� �����)
        {
            actionKey.SetActive(true);
            actionText.SetActive(true);
            extraCross.SetActive(true);
        }
        if (Input.GetButtonDown("Action"))//����� ������ ������ (�)
        {
            if (theDistance < 1.2 &&
                this.GetComponent<BoxCollider>().isTrigger == true
                && GameObject.FindGameObjectWithTag("GameManager").
                GetComponent<Collectables>().getNumberOfNotes() > 0) ;//����� ����� �������
            {
                actionKey.SetActive(false);
                actionText.SetActive(false);
                this.GetComponent<BoxCollider>().gameObject.SetActive(false);
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
    void OnMouseExit()//����� ����� � ��������
    {
        actionKey.SetActive(false);
        actionText.SetActive(false);
        extraCross.SetActive(false);
    }

}

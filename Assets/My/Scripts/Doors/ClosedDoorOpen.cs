using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class ClosedDoorOpen : MonoBehaviour
{
    public float theDistance;//���������� �� �������, ����� �� ����������� � 19 �������

    public GameObject actionKey;//[ e ]
    public GameObject actionText;//"������� �����"
    public GameObject door;//���� �����, ��� �����������
    public GameObject doorTrigger;//������� �� �����
    public GameObject extraCross;//�������������� ������ � �������
    public TMP_Text subtitle;//�������������� ������ � �������


    void Update()
    {
        theDistance = PlayerCasting.DistanceFromTarget;
    }
    void OnMouseOver()
    {
        if (theDistance < 1.2 && doorTrigger.GetComponent<BoxCollider>().isTrigger == true) //����� ������ ��������� ������ (����� ������� �� �����)
        {
            actionKey.SetActive(true);
            actionText.SetActive(true);
            extraCross.SetActive(true);
        }
        if (Input.GetButtonDown("Action"))//����� ������ ������ (�)
        {
            if (theDistance < 1.2 && doorTrigger.GetComponent<BoxCollider>().isTrigger == true)//����� ����� �������
            {
                actionKey.SetActive(false);
                actionText.SetActive(false);
                doorTrigger.SetActive(false);
                doorTrigger.GetComponent<BoxCollider>().isTrigger = false;
                StartCoroutine(theSequence());
            }
        }
    }
     void OnMouseExit()//����� ����� � ��������
    {
        actionKey.SetActive(false);
        actionText.SetActive(false);
        extraCross.SetActive(false);
    }

    IEnumerator theSequence()
    {
        subtitle.text = "��, �������, ����� �������� ����� �������.";
        yield return new WaitForSeconds(3f);
        subtitle.text = " ";
    }
}

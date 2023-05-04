using UnityEngine;

public class DoorCellOpen : MonoBehaviour
{
    public float theDistance;//���������� �� �������, ����� �� ����������� � 19 �������

    public GameObject actionKey;//[ e ]
    public GameObject actionText;//"������� �����"
    public GameObject door;//���� �����, ��� �����������
    public GameObject doorTriiger;//���� �����, ��� �����������
    public GameObject extraCross;//�������������� ������ � �������

    public AudioSource openSound;//���� �������� �����

    void Update()
    {
        theDistance = PlayerCasting.DistanceFromTarget;
    }
    void OnMouseOver()
    {
        if (theDistance < 1.2 && doorTriiger.GetComponent<BoxCollider>().isTrigger == true) //����� ������ ��������� ������ (����� ������� �� �����)
        {
            actionKey.SetActive(true);
            actionText.SetActive(true);
            extraCross.SetActive(true);
        }
        if (Input.GetButtonDown("Action"))//����� ������ ������ (�)
        {
            if (theDistance < 1.2 && doorTriiger.GetComponent<BoxCollider>().isTrigger == true)//����� ����� �������
            {
                actionKey.SetActive(false);
                actionText.SetActive(false);
                door.GetComponent<Animation>().Play("FirstDoorOpenAnim");
                openSound.Play();
                doorTriiger.GetComponent<BoxCollider>().isTrigger = false;
                doorTriiger.SetActive(false);
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

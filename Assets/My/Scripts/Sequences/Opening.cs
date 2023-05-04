using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Opening : MonoBehaviour
{
    public GameObject player;
    public TMP_Text subtitleText;

    void Start()
    {
        player.GetComponent<FirstPersonContoller>().enabled = false;//���� �������� ��� ����������
        StartCoroutine(ScenePlayer());  

    }
    IEnumerator ScenePlayer()
    {
        yield return new WaitForSeconds(1f);//���� �������
        subtitleText.text = "�� �� ������� ��������� ��...";//�����
        yield return new WaitForSeconds(2f);//���� ��� ����������
        subtitleText.text = "";//������� �����
        player.GetComponent<FirstPersonContoller>().enabled = true;//���� �������� ��� ����������
    }

}




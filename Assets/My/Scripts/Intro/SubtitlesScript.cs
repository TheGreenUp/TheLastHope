using System.Collections;
using UnityEngine;
using TMPro;

public class SubtitlesScript : MonoBehaviour
{
    public TMP_Text textBox;
    public bool enableSub = true;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(theSequence());
    }

    IEnumerator theSequence()
    {
        yield return new WaitForSeconds(10f);
        textBox.text = "���, 18 ��� 2022-��, 04:22";
        yield return new WaitForSeconds(5.3f);
        textBox.text = "� ������� � ������� ��������, ������� ������ ����.";
        yield return new WaitForSeconds(3.1f);
        textBox.text = "�� ������ ������, ������� �� ��������� �� �����, �� ���������.";
        yield return new WaitForSeconds(3.2f);
        textBox.text = "��������� � �������, ��� �������� ���� �������� ������� � ���, ��� ������ ������� ��. ";
        yield return new WaitForSeconds(4f);
        textBox.text = "�������� ���������� ������ ��������� ���������� ������ ��-�� �� �����������.";
        yield return new WaitForSeconds(3.56f);
        textBox.text = "� �� ��� � ��� ��������, ��� �������� ����� ��� ������ ������.";
        yield return new WaitForSeconds(4.55f);
        textBox.text = "��������� � ����������, � ������ ��� �������� ������ �� ��������� ������, ������� ������ ������ ��������.";
        yield return new WaitForSeconds(4.8f);
        textBox.text = "���� ��������� ����� � �������� �� ��, ����� ��������� �������� ��� ������ ������������� �������,\n �� ��� �� ��� ������.";
        yield return new WaitForSeconds(5.7f);
        textBox.text = "��� ���������� ���� �����������, � � ��� �� ��������, ��� �������� �� ������, � ������� �� ��������, ���� ����������� ���������, ��� ���������� �������� ����.";
        yield return new WaitForSeconds(8.1f);
        textBox.text = "� ����� ����������� ����, ����� ������� ���������.";
        yield return new WaitForSeconds(2.8f);
        textBox.text = "��� ������ ������� ������ ���� ����, ������ �����,\n �� ������ ������ � ����� � ����� ����������� �����.";
        yield return new WaitForSeconds(5.5f);
        textBox.text = "���� ������ ���� ���������� ����, ��� � ��� ��������, ���� ��� ����������� �� ����� ����.";
        yield return new WaitForSeconds(6f); 
        textBox.text = "��� ������������ � ���� ��� ����������.";
        yield return new WaitForSeconds(3f);
        textBox.text = "";
    }
    public void SwitchSub()
    {
        if (enableSub == false)
        {
            textBox.enabled = true;
            enableSub = true;
        }
        if (enableSub == false)
        {
            textBox.enabled = false;
            enableSub = false;
        }
    }
}

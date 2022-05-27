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
        player.GetComponent<FirstPersonContoller>().enabled = false;//даем контроль над персонажем
        StartCoroutine(ScenePlayer());  

    }
    IEnumerator ScenePlayer()
    {
        yield return new WaitForSeconds(1f);//ждем секунду
        subtitleText.text = "Ну че залупыш проснулся да...";//текст
        yield return new WaitForSeconds(2f);//ждем еще полсекунды
        subtitleText.text = "";//убираем текст
        player.GetComponent<FirstPersonContoller>().enabled = true;//даем контроль над персонажем
    }

}




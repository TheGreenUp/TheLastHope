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
        textBox.text = "“ак, 18 ма€ 2022-го, 04:22";
        yield return new WaitForSeconds(5.3f);
        textBox.text = "я работаю в обычной компании, котора€ делает игры.";
        yield return new WaitForSeconds(3.1f);
        textBox.text = "Ќо каждый проект, который мы выпускаем на рынок, не окупаетс€.";
        yield return new WaitForSeconds(3.2f);
        textBox.text = "ѕозавчера € услышал, как директор моей компании говорит о том, что думает закрыть ее. ";
        yield return new WaitForSeconds(4f);
        textBox.text = "ѕоловина инвесторов просто перестают вкладывать деньги из-за не окупаемости.";
        yield return new WaitForSeconds(3.56f);
        textBox.text = "я не мог в это поверить, эта компани€ стала мне родным местом.";
        yield return new WaitForSeconds(4.55f);
        textBox.text = "ѕоговорив с директором, € убедил его выделить деньги на последний проект, который сможет спасти компанию.";
        yield return new WaitForSeconds(4.8f);
        textBox.text = "¬есь вчерашний вечер € потратил на то, чтобы придумать сценарий дл€ нашего исторического проекта,\n но это не так просто.";
        yield return new WaitForSeconds(5.7f);
        textBox.text = "ћне необходимо было вдохновение, и € тут же вспомнил, что недалеко от города, в которым мы работаем, есть заброшенное поселение, где происход€т странные вещи.";
        yield return new WaitForSeconds(8.1f);
        textBox.text = "я решил отправитьс€ туда, чтобы набрать материала.";
        yield return new WaitForSeconds(2.8f);
        textBox.text = "ƒл€ пущего эффекта поехал туда один, поздно ночью,\n не сказав никому и слова о своем грандиозном плане.";
        yield return new WaitForSeconds(5.5f);
        textBox.text = "»гра должна была запомнитс€ всем, кто в нее поиграет, ведь все происходило на самом деле.";
        yield return new WaitForSeconds(6f); 
        textBox.text = "ƒл€ документации € буду все записывать.";
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

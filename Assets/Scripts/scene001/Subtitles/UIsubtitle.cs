using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIsubtitle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText = default;

    public static UIsubtitle instance;

    private void Awake()
    {
        instance = this;
        ClearSubtitle();
    }
    public void SetSubtitle(string subtitle, float delay)
    {
        subtitleText.text = subtitle;
        StartCoroutine(ClearAfterSeconds(delay));
    }
    public void ClearSubtitle()
    {
        subtitleText.text = "";
    }

    private IEnumerator ClearAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearSubtitle();
    }
}



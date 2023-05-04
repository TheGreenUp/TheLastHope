using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candle_behaviour : MonoBehaviour
{
    private float now = 0f;
    private float last = 0f;
    private float beforelast = 0f;
    void LateUpdate()
    {
        now = 20f + Random.Range(2, 5) + Mathf.PingPong(Time.time * 10, 10f);
        GetComponent<Light>().range = (last + now + beforelast)/ 3;
        last = now;
        if (beforelast != last)
        {
            beforelast = last;
        }
    }
}

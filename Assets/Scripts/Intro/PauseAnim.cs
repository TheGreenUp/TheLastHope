using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PauseAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator carAnimator;

    bool isPaused = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                carAnimator.speed = 0f;
                isPaused = true;
            }
            else
            {
                carAnimator.speed = 1f;
                isPaused = false;
            }
        }
    }
}

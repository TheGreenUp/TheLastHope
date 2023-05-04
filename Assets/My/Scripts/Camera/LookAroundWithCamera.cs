using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundWithCamera : MonoBehaviour
{

    [Header("Look Parametrs")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;



    private Camera playerCamera;

    private float rotationX = 0;

    // Start is called before the first frame update
    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseLock();
    }

    private void HandleMouseLock()//”Ô‡‚ÎÂÌËÂ Ï˚¯ÓÈ
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY; //rotation on x (up & down);
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);// Õ”∆ÕŒ –¿«Œ¡–¿“‹—ﬂ ◊≈ «¿  À¿Ãœ
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);//ıÁ
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);//ıÁ
    }
}

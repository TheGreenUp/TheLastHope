using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_camera : MonoBehaviour
{

    //cursor lock, pozdeji odemykat na inventar?
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
            
    }

    public float m_sensitivity = 0.5f; //todo nastaveni globalne
    private float m_mouse_x;
    private float m_mouse_y;
    private void LateUpdate()
  {
      m_mouse_x += (Input.GetAxisRaw("Mouse X")* m_sensitivity);
      m_mouse_y -= (Input.GetAxisRaw("Mouse Y")* m_sensitivity);
      
      Quaternion camera_rotation = Quaternion.Euler(m_mouse_y, 0f, 0f);
      Quaternion player_rotation = Quaternion.Euler(0f, m_mouse_x, 0f);

      
      transform.localRotation = camera_rotation;
      transform.parent.localRotation = player_rotation;

  }
}

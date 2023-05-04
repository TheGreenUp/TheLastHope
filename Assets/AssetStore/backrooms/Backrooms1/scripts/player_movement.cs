using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
   private CharacterController m_controller;
   public float m_speed = 0.7f;
   private void Awake()
   {
       m_controller = GetComponent<CharacterController>();
   }

   private void FixedUpdate()
   {
       float horizontal = Input.GetAxisRaw("Horizontal");
       float vertical = Input.GetAxisRaw("Vertical");
       Vector3 move_dir = (horizontal*transform.right) + (vertical * transform.forward);
       move_dir *= m_speed;
       m_controller.Move(move_dir * m_speed);
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefMovementScript : MonoBehaviour {
      private Vector2 movement;
      private CharacterController controller;
      private Camera mainCamera;

      public float moveSpeed = 0.1f;
      public float turnSpeed = 5f;

      private void Start() {
            controller = GetComponent<CharacterController>();
      }

      private void FixedUpdate() {
            Vector3 move = new Vector3(0, 0, Input.GetAxis("ThiefMove"));
            Vector3 twist = new Vector3(0, Input.GetAxis("ThiefTurn"), 0);
            controller.Move(transform.rotation * move * moveSpeed);
            transform.Rotate(turnSpeed * twist);



      }
}

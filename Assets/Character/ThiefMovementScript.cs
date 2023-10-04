using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefMovementScript : MonoBehaviour {
      private Vector2 movement;
      private CharacterController controller;
      private Camera mainCamera;

      public float speed = 0.1f;
      public float camSpeed = 5f;

      private void Start() {
            controller = GetComponent<CharacterController>();
      }

      private void FixedUpdate() {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 twist = new Vector3(0, Input.GetAxis("Mouse X"), 0);
            controller.Move(transform.rotation * move * speed);
            transform.Rotate(camSpeed * twist);
      }
}

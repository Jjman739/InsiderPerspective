using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour {
      private CharacterController controller;

      public float turnSpeed = 5f;

      private void Start() {
            controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
      }

      private void FixedUpdate() {
            Vector3 euler = transform.rotation.eulerAngles;
            euler += turnSpeed * new Vector3(Input.GetAxis("GuardY"), Input.GetAxis("GuardX"), 0);
            if (euler.x > 45 && euler.x < 180) {
                  euler.x = 45;
            } else if (euler.x < 315 && euler.x > 180) {
                  euler.x = 315;
            }
            transform.rotation = Quaternion.Euler(euler.x, euler.y, 0);
      }
}

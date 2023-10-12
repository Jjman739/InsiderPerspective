using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour {
      private bool interacting = false;

      public float turnSpeed = 5f;

      private void Start() {
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

      private void Update() {
            if (Input.GetButton("GuardInteract")) {
                  if (!interacting) {
                        interacting = true;
                        Vector3 origin = gameObject.transform.position;
                        Vector3 direction = gameObject.transform.forward;
                        RaycastHit hitInfo;
                        float maxDistance = 100f;
                        if (Physics.Raycast(origin, direction, out hitInfo, maxDistance)) {
                              Debug.Log("Hit "+hitInfo.collider.name);
                        } else {
                              Debug.Log("No hit.");
                        }
                  }
            } else {
                  if (interacting) {
                        interacting = false;
                  }
            }
      }
}

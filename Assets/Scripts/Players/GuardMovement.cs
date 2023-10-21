using Unity.Netcode;
using UnityEngine;

public class GuardMovement : NetworkBehaviour {
    private CharacterController controller;

    [SerializeField] private float turnSpeed = 5f;

    private void Start()
    {
        if (!IsHost && !IsOwner) return;

        controller = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!IsHost && !IsOwner) return;

        Vector3 euler = transform.rotation.eulerAngles;
        euler += turnSpeed * new Vector3(Input.GetAxis("GuardY"), Input.GetAxis("GuardX"), 0);
        if (euler.x > 45 && euler.x < 180) {
            euler.x = 45;
        } else if (euler.x < 315 && euler.x > 180) {
            euler.x = 315;
        }
        transform.rotation = Quaternion.Euler(euler.x, euler.y, 0);
    }

    public void SetTurnSpeed(float speed) { turnSpeed = speed; }
}

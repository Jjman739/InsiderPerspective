using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ThiefMovementScript : NetworkBehaviour
{
    private Vector3 move;
    private Vector3 twist;
    private CharacterController controller;
    private GameObject cameraHolder;
    private Camera mainCamera;

    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float turnSpeed = 5f;

    private void Start()
    {
        if (!IsClient && !IsOwner) return;
        
        controller = GetComponent<CharacterController>();
        cameraHolder = transform.GetChild(0).gameObject;
        cameraHolder.SetActive(IsOwner);
    }

    private void FixedUpdate()
    {
        if (!IsClient && !IsOwner) return;

        move = new Vector3(0, 0, Input.GetAxis("ThiefMove"));
        twist = new Vector3(0, Input.GetAxis("ThiefTurn"), 0);

        controller.Move(transform.rotation * move * moveSpeed);
        transform.Rotate(turnSpeed * twist);
    }

    public void SetMoveSpeed(float speed) { moveSpeed = speed; }
    public void SetTurnSpeed(float speed) { turnSpeed = speed; }
}

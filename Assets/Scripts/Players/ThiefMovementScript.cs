using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ThiefMovementScript : MonoBehaviour
{
    private Vector3 move;
    private Vector3 twist;
    private CharacterController controller;
    private float currentAlertTimer;
    private bool inLineOfSight;

    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float alertTimer = 2f;
    [SerializeField] private Slider alertMeter;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentAlertTimer = alertTimer;
    }

    private void FixedUpdate()
    {
        move = new Vector3(0, 0, Input.GetAxis("ThiefMove"));
        twist = new Vector3(0, Input.GetAxis("ThiefTurn"), 0);

        controller.Move(transform.rotation * move * moveSpeed);
        transform.Rotate(turnSpeed * twist);

        if (inLineOfSight)
        {
            currentAlertTimer -= Time.deltaTime;
            if (currentAlertTimer <= 0)
            {
                Debug.Log("Caught");
                Destroy(this);
            }
        }
        else if (currentAlertTimer < alertTimer)
        {
            currentAlertTimer += Time.deltaTime;
            Mathf.Min(currentAlertTimer, alertTimer);
        }

        alertMeter.value = alertTimer - currentAlertTimer;
    }

    public void SetMoveSpeed(float speed) { moveSpeed = speed; }
    public void SetTurnSpeed(float speed) { turnSpeed = speed; }
    public void SetInLineOfSight(bool inSight) { inLineOfSight = inSight; }
}

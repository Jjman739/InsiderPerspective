using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThiefMovementScript : MonoBehaviour
{
    private Vector3 move;
    private Vector3 twist;
    private CharacterController controller;
    private AudioSource audioSource;
    private float currentAlertTimer;
    private bool inLineOfSight;

    private float chargeTimer = 60;

    private float jumpTimer;
    private float jumpSpeed;

    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float jumpHeight = .5f;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private int charge = 5;
    [SerializeField] private float alertTimer = 2f;
    [SerializeField] private Slider alertMeter;
    [SerializeField] private AudioClip robotJump;
    [SerializeField] private AudioClip robotWalk;

    public string forwardButton = "ThiefMoveUp";
    public string backwardButton = "ThiefMoveDown";
    public string leftButton = "ThiefMoveLeft";
    public string rightButton = "ThiefMoveRight";
    public string jumpButton = "Jump";

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentAlertTimer = alertTimer;
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (GameObject.Find("PauseControl").GetComponent<PauseMenu>().paused)
        {
            return;
        }

        float moveAxis = 0;
        if (Input.GetButton(forwardButton)) { moveAxis += 1; }
        if (Input.GetButton(backwardButton)) { moveAxis -= 1; }
        float turnAxis = 0;
        if (Input.GetButton(leftButton)) { turnAxis -= 1; }
        if (Input.GetButton(rightButton)) { turnAxis += 1; }

        move = new Vector3(0, 0, moveAxis);
        twist = new Vector3(0, turnAxis, 0);

        //jump control
        bool grounded = controller.isGrounded;

        if (grounded && move != Vector3.zero)
        {   
            audioSource.clip = robotWalk;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

        if (grounded)
        {
            jumpTimer = .2f;
        }

        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (grounded && jumpSpeed < 0)
        {
            jumpSpeed = 0f;
        }

        jumpSpeed += gravity * Time.deltaTime;

        if (jumpTimer > 0 && Input.GetButton(jumpButton))
        {
            jumpTimer = 0;
            jumpSpeed = Mathf.Sqrt(jumpHeight * -gravity * 2);
            audioSource.clip = robotJump;
            audioSource.loop = false;
            audioSource.Play();
        }

        move.y = jumpSpeed;

        controller.Move(transform.rotation * move * moveSpeed);
        transform.Rotate(turnSpeed * twist);

        if (inLineOfSight)
        {
            currentAlertTimer -= Time.deltaTime;
            if (currentAlertTimer <= 0)
            {
                Debug.Log("Caught");
                SceneManager.LoadScene("LoseScene");
            }
        }
        else if (currentAlertTimer < alertTimer)
        {
            currentAlertTimer += Time.deltaTime;
            Mathf.Min(currentAlertTimer, alertTimer);
        }

        alertMeter.value = alertTimer - currentAlertTimer;

        //timer to reduce charge
        if (chargeTimer > 0)
        {
            chargeTimer -= Time.deltaTime;
        }
        else
        {
            chargeTimer = 60;
            if(chargeTimer > 1)
            {
                charge -= 1;
            }
            else
            {
                Debug.Log("Out of battery.");
                SceneManager.LoadScene("LoseScene");
            }
        }

    }

    public void SetMoveSpeed(float speed) { moveSpeed = speed; }
    public void SetTurnSpeed(float speed) { turnSpeed = speed; }
    public void SetInLineOfSight(bool inSight) { inLineOfSight = inSight; }
    public void SetAlertTimer(float time) { alertTimer = time; }
}

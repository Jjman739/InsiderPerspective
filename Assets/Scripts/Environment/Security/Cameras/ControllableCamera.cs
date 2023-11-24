using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableCamera : MonoBehaviour
{
    [SerializeField] private float rotMinX;
    [SerializeField] private float rotMaxX;
    [SerializeField] private float rotMinY;
    [SerializeField] private float rotMaxY;
    private float rotX;
    private float rotY;

    private enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE
    }
    private float rotationInterval = 0.2f;
    private float delayTimer = 0.5f;
    private float currentDelayTimer;
    private new Camera camera;
    private RenderTexture targetTexture;
    private AudioListener audioListener;
    private bool shouldMove;
    private bool isMoving;
    private Direction moveDirection;
    private bool viewing;
    private bool broken;
    [SerializeField] private bool isFishEye;

    private void Start()
    {
        camera = GetComponent<Camera>();
        audioListener = GetComponent<AudioListener>();
        targetTexture = camera.targetTexture;
        currentDelayTimer = delayTimer;

        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        if (camera.targetTexture is null)
        {
            camera.enabled = false;
        }
    }

    private void Update()
    {
        if (camera.enabled && camera.targetTexture is null && !viewing)
        {
            camera.enabled = false;
        }

        audioListener.enabled = viewing;

        checkIsMoving();

        if (isMoving)
        {
            if (moveDirection == Direction.UP)
            {
                moveVertical(false);
            }

            if (moveDirection == Direction.DOWN)
            {
                moveVertical(true);
            }

            if (moveDirection == Direction.LEFT)
            {
                moveHorizontal(false);
            }

            if (moveDirection == Direction.RIGHT)
            {
                moveHorizontal(true);
            }
        }
    }

    private void moveHorizontal(bool positive)
    {
        if (positive)
        {
            rotY = Mathf.Min(rotMaxY, rotY + rotationInterval);
        } else
        {
            rotY = Mathf.Max(rotMinY, rotY - rotationInterval);
        }
        transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
    }

    private void moveVertical(bool positive)
    {
        if (positive)
        {
            rotX = Mathf.Min(rotMaxX, rotX + rotationInterval);
        }
        else
        {
            rotX = Mathf.Max(rotMinX, rotX - rotationInterval);
        }
        transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
    }

    private void checkIsMoving()
    {
        if ((shouldMove && !isMoving) || (!shouldMove && isMoving))
        {
            currentDelayTimer -= Time.deltaTime;

            if (currentDelayTimer <= 0)
            {
                isMoving = shouldMove;
            }
        }
        else
        {
            currentDelayTimer = delayTimer;
        }
    }

    public void EnterView()
    {
        camera.targetTexture = null;
        camera.enabled = true;
        viewing = true;
    }

    public void ExitView()
    {
        camera.targetTexture = targetTexture;
        camera.enabled = true;
        viewing = false;
    }

    public void SwapView()
    {
        camera.enabled = false;
        viewing = false;
    }
    
    public void SetMoveUp(bool moving)
    {
        moveDirection = Direction.UP;
        shouldMove = moving;
    }

    public void SetMoveDown(bool moving)
    {
        moveDirection = Direction.DOWN;
        shouldMove = moving;
    }

    public void SetMoveLeft(bool moving)
    {
        moveDirection = Direction.LEFT;
        shouldMove = moving;
    }

    public void SetMoveRight(bool moving)
    {   
        moveDirection = Direction.RIGHT;
        shouldMove = moving;
    }

    public void Break()
    {
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = Color.black;
        camera.cullingMask = 0;
        broken = true;
    }

    public Camera GetCamera() { return camera; }
    public Transform GetCameraGroup() { return transform.parent; }
    public int GetCameraGroupIndex() { return transform.GetSiblingIndex(); }
    public bool GetBroken() { return broken; }
    public void SetBroken(bool broken) { this.broken = broken; }
    public bool IsFishEye() { return isFishEye; }
}

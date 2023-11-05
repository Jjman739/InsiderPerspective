using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableCamera : MonoBehaviour
{
    private float rotationInterval = 10f;
    private new Camera camera;
    private RenderTexture targetTexture;
    private bool viewing;
    private bool broken;

    private void Start()
    {
        camera = GetComponent<Camera>();
        targetTexture = camera.targetTexture;
    }

    private void Update()
    {
        if (camera.targetTexture is null && !viewing)
        {
            camera.enabled = false;
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

    public void MoveHorizontal(bool positive)
    {
        if (broken) return;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + (rotationInterval * (positive ? 1 : -1)), transform.eulerAngles.z);
    }

    public void MoveVertical(bool positive)
    {
        if (broken) return;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + (rotationInterval * (positive ? 1 : -1)), transform.eulerAngles.y, transform.eulerAngles.z);
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
}

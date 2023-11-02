using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableCamera : MonoBehaviour
{
    private new Camera camera;
    private RenderTexture targetTexture;
    private bool viewing;

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
        camera.enabled = false;
        viewing = false;
    }

    public Camera GetCamera() { return camera; }
}

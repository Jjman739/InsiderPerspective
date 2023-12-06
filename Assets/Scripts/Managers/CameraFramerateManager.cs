using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFramerateManager : MonoBehaviour
{
    private AttachCamera[] cameras;
    private int camIndex = 0;

    void Start()
    {
        cameras = FindObjectsOfType<AttachCamera>();
    }

    void Update()
    {
        cameras[camIndex].RenderFrame();
        camIndex++;
        if (camIndex >= cameras.Length)
        {
            camIndex = 0;
        }
    }
}

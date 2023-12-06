using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFramerateManager : MonoBehaviour
{
    private AttachCamera[] cameras;
    private int camIndex = 0;

    [SerializeField] private int RendersPerFrame = 1; 

    void Start()
    {
        cameras = FindObjectsOfType<AttachCamera>();
    }

    void Update()
    {
        for (int i = 0; i < RendersPerFrame; i++)
        {
            cameras[camIndex].RenderFrame();
            camIndex++;
            if (camIndex >= cameras.Length)
            {
                camIndex = 0;
            }
        }
    }
}

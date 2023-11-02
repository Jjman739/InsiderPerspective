using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewer : Singleton<CameraViewer>
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject cameraUI;
    private ControllableCamera currentCamera;

    private void Start()
    {
        cameraUI.SetActive(false);
    }

    public void ViewCamera(ControllableCamera camera)
    {
        Cursor.lockState = CursorLockMode.None;
        camera.EnterView();
        cameraUI.SetActive(true);
        mainCamera.enabled = false;
        currentCamera = camera;
    }

    public void ExitCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentCamera.ExitView();
        cameraUI.SetActive(false);
        mainCamera.enabled = true;
        currentCamera = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachCamera : MonoBehaviour
{
    [SerializeField] private Camera attachedCamera;
    private GameObject monitor;

    private RenderTexture texture;

    private void Start()
    {
        monitor = transform.GetChild(0).gameObject;

        texture = new RenderTexture(256, 256, 0);
        monitor.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", texture);
        attachedCamera.targetTexture = texture;
        attachedCamera.enabled = true;
    }

    public Camera GetAttachedCamera() { return attachedCamera; }

    public void SetAttachedCamera(Camera newCamera)
    {
        attachedCamera.targetTexture = null;
        attachedCamera = newCamera;
        newCamera.targetTexture = texture;
    }

    public ControllableCamera GetControllableCamera()
    {
        return attachedCamera.GetComponent<ControllableCamera>();
    }
}
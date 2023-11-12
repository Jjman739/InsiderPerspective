using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachCamera : MonoBehaviour
{
    [SerializeField] private Camera attachedCamera;
    private GameObject monitor;

    private void Start()
    {
        monitor = transform.GetChild(0).gameObject;

        RenderTexture texture = new RenderTexture(256, 256, 0);
        monitor.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", texture);
        attachedCamera.targetTexture = texture;
    }

    public Camera GetAttachedCamera() { return attachedCamera; }
}

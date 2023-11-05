using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectMonitor : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;

    private GameObject containerCube;
    private ControllableCamera attachedCamera;

    private void Start()
    {
        containerCube = transform.Find("ContainerCube").gameObject;
        attachedCamera = GetComponent<AttachCamera>().GetAttachedCamera().GetComponent<ControllableCamera>();
    }

    private void OnMouseEnter()
    {        
        containerCube.GetComponent<MeshRenderer>().material = selectedMaterial;
    }

    private void OnMouseExit()
    {
        containerCube.GetComponent<MeshRenderer>().material = defaultMaterial;
    }

    private void OnMouseDown()
    {
        CameraViewer.Instance.ViewCamera(attachedCamera);
    }
}

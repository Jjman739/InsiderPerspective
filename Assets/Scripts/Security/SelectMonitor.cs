using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectMonitor : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;

    private GameObject containerCube;
    private AttachCamera attachCamera;

    private void Start()
    {
        containerCube = transform.Find("ContainerCube").gameObject;
        attachCamera = GetComponent<AttachCamera>();
    }

    private void OnMouseEnter()
    {
        if (GameObject.Find("PauseControl").GetComponent<PauseMenu>().paused)
        {
            return;
        }
        containerCube.GetComponent<MeshRenderer>().material = selectedMaterial;
    }

    private void OnMouseExit()
    {
        if (GameObject.Find("PauseControl").GetComponent<PauseMenu>().paused)
        {
            return;
        }

        containerCube.GetComponent<MeshRenderer>().material = defaultMaterial;
    }

    private void OnMouseDown()
    {
        if (GameObject.Find("PauseControl").GetComponent<PauseMenu>().paused)
        {
            return;
        }

        CameraViewer.Instance.ViewCamera(attachCamera.GetControllableCamera(), attachCamera);
    }
}

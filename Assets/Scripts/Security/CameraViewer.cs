using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraViewer : Singleton<CameraViewer>
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject cameraUI;
    [SerializeField] private GameObject cameraSwapButtonPrefab;
    private Transform cameraSwapButtons;
    private Transform currentCameraGroup;
    private ControllableCamera currentCamera;
    private int currentCameraIndex;

    private void Start()
    {
        cameraUI.SetActive(false);
        cameraSwapButtons = cameraUI.transform.Find("SwapButtons");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            BreakCamera(currentCameraIndex);
        }
    }

    public void ViewCamera(ControllableCamera camera)
    {
        Cursor.lockState = CursorLockMode.None;
        
        camera.EnterView();
        cameraUI.SetActive(true);
        mainCamera.enabled = false;

        currentCamera = camera;
        currentCameraGroup = camera.GetCameraGroup();
        currentCameraIndex = camera.GetCameraGroupIndex();

        generateCameraGroupButtons();

        SwapCamera(currentCameraIndex);
    }

    public void ExitCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        currentCamera.ExitView();
        cameraUI.SetActive(false);
        mainCamera.enabled = true;
        
        currentCamera = null;
        currentCameraGroup = null;
        currentCameraIndex = -1;
    }

    public void SwapCamera(int index)
    {
        currentCamera.ExitView();
        currentCamera = currentCameraGroup.GetChild(index).GetComponent<ControllableCamera>();
        currentCameraIndex = currentCamera.GetCameraGroupIndex();
        currentCamera.EnterView();
    }

    public void MoveUp()
    {
        currentCamera.MoveVertical(false);
    }

    public void MoveDown()
    {
        currentCamera.MoveVertical(true);
    }

    public void MoveLeft()
    {
        currentCamera.MoveHorizontal(false);
    }

    public void MoveRight()
    {
        currentCamera.MoveHorizontal(true);
    }

    public void BreakCamera(int index)
    {
        currentCameraGroup.GetChild(index).GetComponent<ControllableCamera>().Break();
    }

    private void generateCameraGroupButtons()
    {
        foreach (Transform child in cameraSwapButtons)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentCameraGroup.childCount; i++)
        {
            int cameraIndex = i; //this looks dumb but you need it for the lambda so pretend you didn't see this and move on
            GameObject swapButton = Instantiate(cameraSwapButtonPrefab, cameraSwapButtons);
            swapButton.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
            swapButton.GetComponent<Toggle>().isOn = cameraIndex == currentCameraIndex ? true : false;
            swapButton.GetComponent<Toggle>().onValueChanged.AddListener((b) =>
            {
                if (b)
                {
                    cameraSwapButtons.GetChild(currentCameraIndex).GetComponent<Toggle>().isOn = false;
                    SwapCamera(cameraIndex);
                }
            });
        }
    }

    public bool IsViewing() { return currentCamera is not null; }
}

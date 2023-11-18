using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Enumerations;

public class CameraViewer : Singleton<CameraViewer>
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject cameraUI;
    [SerializeField] private GameObject cameraSwapButtonPrefab;
    [SerializeField] private AudioClip cameraSwivel;
    [SerializeField] private AudioClip cameraClick;
    [SerializeField] private GameObject photoView;
    private Transform cameraSwapButtons;
    private Transform currentCameraGroup;
    private ControllableCamera currentCamera;
    private int currentCameraIndex;
    private AudioSource audioSource;

    private void Start()
    {
        cameraUI.SetActive(false);
        cameraSwapButtons = cameraUI.transform.Find("SwapButtons");
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (currentCamera is null) return;

        if (Input.GetButtonDown("UpArrow"))
        {
            SetMoveUp(true);
        }

        if (Input.GetButtonDown("DownArrow"))
        {
            SetMoveDown(true);
        }

        if (Input.GetButtonDown("LeftArrow"))
        {
            SetMoveLeft(true);
        }

        if (Input.GetButtonDown("RightArrow"))
        {
            SetMoveRight(true);
        }

        if (Input.GetButtonUp("UpArrow"))
        {
            SetMoveUp(false);
        }

        if (Input.GetButtonUp("DownArrow"))
        {
            SetMoveDown(false);
        }

        if (Input.GetButtonUp("LeftArrow"))
        {
            SetMoveLeft(false);
        }

        if (Input.GetButtonUp("RightArrow"))
        {
            SetMoveRight(false);
        }

        if (Input.GetButtonDown("TogglePhotoView"))
        {
            photoView.SetActive(!photoView.activeSelf);
        }

        for (int i = 0; i < 9; i++)
        {
            if (Input.GetButtonDown((i + 1).ToString()))
            {
                cameraSwapButtons.GetChild(i).GetComponent<Toggle>().isOn = true;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.P))
        {
            BreakCamera(currentCameraIndex);
        }*/
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

        DialogueManager.Instance.PlayDialogue(DialogueEvent.VIEW_MONITOR);

        //TODO: play DialogueEvent.VIEW_FISH_EYE_MONITOR if camera is a fish eye camera
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
        audioSource.clip = cameraClick;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void SetMoveUp(bool moving)
    {
        currentCamera.SetMoveUp(moving);
        audioSource.clip = cameraSwivel;
        audioSource.loop = moving;
        if (moving) audioSource.Play(); else audioSource.Stop();
    }

    public void SetMoveDown(bool moving)
    {
        currentCamera.SetMoveDown(moving);
        audioSource.clip = cameraSwivel;
        audioSource.loop = moving;
        if (moving) audioSource.Play(); else audioSource.Stop();
    }

    public void SetMoveLeft(bool moving)
    {
        currentCamera.SetMoveLeft(moving);
        audioSource.clip = cameraSwivel;
        audioSource.loop = moving;
        if (moving) audioSource.Play(); else audioSource.Stop();
    }

    public void SetMoveRight(bool moving)
    {
        currentCamera.SetMoveRight(moving);
        audioSource.clip = cameraSwivel;
        audioSource.loop = moving;
        if (moving) audioSource.Play(); else audioSource.Stop();
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
            swapButton.GetComponent<Toggle>().isOn = cameraIndex == currentCameraIndex;
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

    public bool UsingMain()
    {
        if (mainCamera.enabled == true)
        {
            return true;
        }
        else { return false; }
    }
}

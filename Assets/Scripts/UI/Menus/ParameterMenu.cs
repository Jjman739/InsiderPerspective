using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParameterMenu : MonoBehaviour
{
    [SerializeField] private GameObject thief;
    [SerializeField] private GameObject guard;
    [SerializeField] private GameObject patrollingGuard;

    [SerializeField] private TextMeshProUGUI moveSpeedValueText;
    [SerializeField] private TextMeshProUGUI turnSpeedValueText;
    [SerializeField] private TextMeshProUGUI mouseSensitivityText;
    [SerializeField] private TextMeshProUGUI guardSpeedValueText;
    [SerializeField] private TextMeshProUGUI alertTimeValueText;
    private bool menuOpen = false;

    private void Update()
    {
        if (Input.GetButtonDown("ParameterMenu"))
        {
            if (!menuOpen)
            {
                GetComponent<CanvasGroup>().alpha = 1;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                GetComponent<CanvasGroup>().alpha = 0;
                if (!CameraViewer.Instance.IsViewing())
                    Cursor.lockState = CursorLockMode.Locked;
            }
            menuOpen = !menuOpen;
        }
    }

    public void UpdateThiefMoveSpeed(float speed)
    {
        if (!menuOpen) return;
        thief.GetComponent<ThiefMovementScript>().SetMoveSpeed(speed);
        moveSpeedValueText.text = speed.ToString();
    }

    public void UpdateThiefTurnSpeed(float speed)
    {
        if (!menuOpen) return;
        thief.GetComponent<ThiefMovementScript>().SetTurnSpeed(speed);
        turnSpeedValueText.text = speed.ToString();
    }

    public void UpdateMouseSensitivity(float sensitivity)
    {
        if (!menuOpen) return;
        guard.GetComponent<GuardMovement>().SetTurnSpeed(sensitivity);
        mouseSensitivityText.text = sensitivity.ToString();
    }

    public void UpdateGuardMoveSpeed(float speed)
    {
        if (!menuOpen) return;
        patrollingGuard.GetComponent<PatrollingGuard>().SetMoveSpeed(speed);
        guardSpeedValueText.text = speed.ToString();
    }

    public void UpdateGuardAlertTimer(float time)
    {
        if (!menuOpen) return;
        thief.GetComponent<ThiefMovementScript>().SetAlertTimer(time);
        alertTimeValueText.text = time.ToString();
    }
}

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                GetComponent<CanvasGroup>().alpha = 1;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                GetComponent<CanvasGroup>().alpha = 0;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void UpdateThiefMoveSpeed(float speed)
    {
        thief.GetComponent<ThiefMovementScript>().SetMoveSpeed(speed);
        moveSpeedValueText.text = speed.ToString();
    }

    public void UpdateThiefTurnSpeed(float speed)
    {
        thief.GetComponent<ThiefMovementScript>().SetTurnSpeed(speed);
        turnSpeedValueText.text = speed.ToString();
    }

    public void UpdateMouseSensitivity(float sensitivity)
    {
        guard.GetComponent<GuardMovement>().SetTurnSpeed(sensitivity);
        mouseSensitivityText.text = sensitivity.ToString();
    }

    public void UpdateGuardMoveSpeed(float speed)
    {
        patrollingGuard.GetComponent<PatrollingGuard>().SetMoveSpeed(speed);
        guardSpeedValueText.text = speed.ToString();
    }

    public void UpdateGuardAlertTimer(float time)
    {
        thief.GetComponent<ThiefMovementScript>().SetAlertTimer(time);
        alertTimeValueText.text = time.ToString();
    }
}

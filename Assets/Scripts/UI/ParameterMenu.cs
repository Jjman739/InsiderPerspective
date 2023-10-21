using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ParameterMenu : MonoBehaviour
{
    private const string thiefString = "THIEF";
    private const string guardString = "GUARD";
    private string playerString;
    private GameObject localPlayer;

    [SerializeField] private TextMeshProUGUI playerTypeText;
    [SerializeField] private TextMeshProUGUI moveSpeedValueText;
    [SerializeField] private TextMeshProUGUI turnSpeedValueText;

    public void Initialize()
    {
        if (NetworkManager.Singleton.LocalClient.PlayerObject)
            localPlayer = NetworkManager.Singleton.LocalClient.PlayerObject.gameObject;

        playerString = localPlayer && localPlayer.GetComponent<GuardMovement>() ? guardString : thiefString;
        playerTypeText.text = $"Player Type: {playerString}";
    }

    public void UpdatePlayerMoveSpeed(float speed)
    {
        if (localPlayer.GetComponent<ThiefMovementScript>())
        {
            localPlayer.GetComponent<ThiefMovementScript>().SetMoveSpeed(speed);
            moveSpeedValueText.text = speed.ToString();
        }
    }

    public void UpdatePlayerTurnSpeed(float speed)
    {
        localPlayer = NetworkManager.Singleton.LocalClient.PlayerObject.gameObject;
        if (localPlayer.GetComponent<ThiefMovementScript>())
        {
            localPlayer.GetComponent<ThiefMovementScript>().SetTurnSpeed(speed);
            turnSpeedValueText.text = speed.ToString();
        }
        else if (localPlayer.GetComponent<GuardMovement>())
        {
            localPlayer.GetComponent<GuardMovement>().SetTurnSpeed(speed);
            turnSpeedValueText.text = speed.ToString();
        }
    }
}

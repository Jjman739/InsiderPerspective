using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamSwitch : MonoBehaviour
{
    public GameObject fullScreenCanvas;
    public GameObject room;
    public GameObject roomCanvas;

    void Start()
    {
        fullScreenCanvas.SetActive(false);
    }

 

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fullScreenCanvas.SetActive(true);
            room.SetActive(false);
            roomCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }

    }
}

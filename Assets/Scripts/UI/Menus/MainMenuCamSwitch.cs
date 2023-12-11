using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamSwitch : MonoBehaviour
{
    public Canvas UICanvas;

     void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UICanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRenderSwitch : MonoBehaviour
{
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private GameObject SecurityRoom;


    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UICanvas.renderMode = RenderMode.ScreenSpaceCamera;
            Cursor.lockState = CursorLockMode.None;
            SecurityRoom.SetActive(false);

        }
        else if (Input.GetMouseButtonDown(1))
        {
            UICanvas.renderMode = RenderMode.WorldSpace;
            Cursor.lockState = CursorLockMode.Locked;
            SecurityRoom.SetActive(true);
        }
    }
}

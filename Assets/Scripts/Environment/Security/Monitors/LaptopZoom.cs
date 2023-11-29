using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopZoom : MonoBehaviour
{

    public Camera guardCam;
    public float defaultFov;
    public GameObject laptopUI;

    private bool zoomed = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultFov = guardCam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    private void OnMouseDown()
    {
        if (zoomed == false)
        {
            guardCam.fieldOfView = defaultFov / 2.5f;
            zoomed = true;
            Cursor.lockState = CursorLockMode.None;
            laptopUI.SetActive(true);

        }
        else
        {
            guardCam.fieldOfView = defaultFov;
            zoomed = false;
            Cursor.lockState = CursorLockMode.Locked;
            laptopUI.SetActive(false);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopZoom : MonoBehaviour
{

    public Camera guardCam;
    public float defaultFov;
    public GameObject laptopUI;

    // Start is called before the first frame update
    void Start()
    {
        defaultFov = guardCam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            guardCam.fieldOfView = defaultFov / 2.5f;
            Cursor.lockState = CursorLockMode.None;
            laptopUI.SetActive(true);

        }
        else if (Input.GetMouseButtonDown(1))
        {
            guardCam.fieldOfView = defaultFov;
            Cursor.lockState = CursorLockMode.Locked;
            laptopUI.SetActive(false);
        }


    }

}

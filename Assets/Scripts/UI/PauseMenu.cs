using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused;
    public GameObject cameraUI;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        paused = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Pause"))
        {
            if(!paused) 
            {
                Pause();
            }
            else
            {
                Unpause();
            }
            
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        paused = true;
        pauseMenu.SetActive(true);

        if (!GameObject.Find("CameraViewer").GetComponent<CameraViewer>().UsingMain())
        {
            cameraUI.SetActive(false);
        }
    }

    public void Unpause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        paused = false;
        pauseMenu.SetActive(false);

        if (!GameObject.Find("CameraViewer").GetComponent<CameraViewer>().UsingMain())
        {
            cameraUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else { Cursor.lockState = CursorLockMode.Locked; }
    }

    public bool GetPaused()
    {
        return paused;
    }
}

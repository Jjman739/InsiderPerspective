using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private bool paused = false;

    public GameObject pauseMenu = new GameObject();


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Unpause();

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                paused = true;

                pauseMenu.SetActive(true);
            }
        }


    }

    public void Unpause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        paused= false;

    }

}

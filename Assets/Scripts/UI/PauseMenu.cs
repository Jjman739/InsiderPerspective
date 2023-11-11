using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        paused = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!paused) 
            {
                Time.timeScale = 0;
                paused = true;
                pauseMenu.SetActive(true);

            }else
            {
                Time.timeScale = 1;
                paused = false;
                pauseMenu.SetActive(false);

            }
            
        }
    }

    public bool GetPaused()
    {
        return paused;
    }
}

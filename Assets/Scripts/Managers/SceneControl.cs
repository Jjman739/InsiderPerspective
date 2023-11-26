using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Scene current = SceneManager.GetActiveScene();

        Time.timeScale = 1;

        if (current.name != "Main")
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

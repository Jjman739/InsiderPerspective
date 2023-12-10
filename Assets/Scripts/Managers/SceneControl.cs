using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        Debug.Log("loading scene");
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

using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigation : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
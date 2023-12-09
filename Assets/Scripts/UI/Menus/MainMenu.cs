using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructions;
    public GameObject settings;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        instructions.SetActive(false);
        settings.SetActive(false);
        settings.GetComponent<SettingsScript>().SetVolumeSettings();
    }

    public void OpenInstructions()
    {
        mainMenu.SetActive(false);
        instructions.SetActive(true);
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        mainMenu.SetActive(true);
        instructions.SetActive(false);
        settings.SetActive(false);
    }
}

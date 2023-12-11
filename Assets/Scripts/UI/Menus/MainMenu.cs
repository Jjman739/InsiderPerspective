using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameModes;
    public GameObject instructions;
    public GameObject settings;
    public TMP_InputField seedInput;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        gameModes.SetActive(false);
        instructions.SetActive(false);
        settings.SetActive(false);
        settings.GetComponent<SettingsScript>().SetVolumeSettings();
    }

    public void OpenGameModes()
    {
        mainMenu.SetActive(false);
        gameModes.SetActive(true);
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
        gameModes.SetActive(false);
        instructions.SetActive(false);
        settings.SetActive(false);
    }

    public void SetSeed()
    {
        int seed;
        if (int.TryParse(seedInput.text, out seed))
        {
            Debug.Log("setting seed");
            Debug.Log(seed);
            Random.InitState(seed);
        }
    }
}

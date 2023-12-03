using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructions;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        instructions.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInstructions()
    {
        instructions.SetActive(true) ;
        mainMenu.SetActive(false) ;
    }

    public void CloseInstructions()
    {
        instructions.SetActive(false) ;
        mainMenu.SetActive(true) ;
    }
}

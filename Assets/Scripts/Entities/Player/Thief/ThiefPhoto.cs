using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefPhoto : MonoBehaviour
{
    [SerializeField] private float photoCooldown = 1f;
    [SerializeField] private Camera photoCamera;
    private bool photoActive = false;
    private float photoCooldownTimer = 0;
    private GameObject pauseControl;
    public string photoButton = "ThiefPhoto";

    private void Start()
    {
        pauseControl = GameObject.Find("PauseControl");
    }

    void Update()
    {

        if (pauseControl is not null && pauseControl.GetComponent<PauseMenu>().paused)
        {
            return;
        }

        photoCooldownTimer -= Time.deltaTime;
        if (Input.GetButton(photoButton) != photoActive)
        {
            photoActive = Input.GetButton(photoButton);
            if (photoActive && photoCooldownTimer <= 0)
            {
                TakePhoto();
            }
        }
    }

    private void TakePhoto()
    {
        photoCooldownTimer = photoCooldown;
        photoCamera.Render();
    }
}

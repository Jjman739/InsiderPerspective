using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefPhoto : MonoBehaviour
{
    [SerializeField] private float photoCooldown = 1f;
    private bool photoActive = false;
    private float photoCooldownTimer = 0;

    void Update()
    {
        photoCooldownTimer -= Time.deltaTime;
        if (Input.GetButton("ThiefPhoto") != photoActive)
        {
            photoActive = Input.GetButton("ThiefPhoto");
            if (photoActive && photoCooldownTimer <= 0)
            {
                TakePhoto();
            }
        }
    }

    private void TakePhoto()
    {
        photoCooldownTimer = photoCooldown;
        Debug.Log("Click!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerations;

public class SurvivalOnly : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance.GetCurrentGameMode() != GameMode.SURVIVAL)
        {
            Destroy(gameObject);
        }
    }
}

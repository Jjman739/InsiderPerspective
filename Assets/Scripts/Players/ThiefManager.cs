using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThiefManager : MonoBehaviour
{
    public void TakeDamage()
    {
        Debug.Log("Hit a trap.");
        SceneManager.LoadScene("LoseScene");
    }
}

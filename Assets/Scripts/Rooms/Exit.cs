using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        ThiefTreasure thiefTreasure = other.gameObject.GetComponent<ThiefTreasure>();
        Debug.Log(thiefTreasure);
        if (thiefTreasure != null)
        {
            Debug.Log(thiefTreasure.treasureCount);
            Debug.Log(thiefTreasure.goal);
            if (thiefTreasure.treasureCount >= thiefTreasure.goal)
            {
                  Debug.Log("Win!");
                  SceneManager.LoadScene("WinScene");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecurityDoor : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        ThiefManager thief = other.gameObject.GetComponent<ThiefManager>();
        if (thief != null)
        {
            thief.AttemptWin();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThiefManager : MonoBehaviour
{
    [SerializeField] private Transform cameraRoot;

    public void TakeDamage()
    {
        Debug.Log("Hit a trap.");
        cameraRoot.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }
}

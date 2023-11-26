using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;

    void Start()
    {
        cam.Render();
    }
}

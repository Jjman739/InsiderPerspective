using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnStart : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }
}

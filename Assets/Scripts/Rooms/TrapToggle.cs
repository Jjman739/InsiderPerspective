using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapToggle : MonoBehaviour
{
    public List<GameObject> toggleObjects;

    public void TrapDisable()
    {
        foreach(GameObject item in toggleObjects)
        {
            GameObject.Destroy(item);
        }
    }
}

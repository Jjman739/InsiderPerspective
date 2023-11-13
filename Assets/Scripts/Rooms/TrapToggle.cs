using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapToggle : MonoBehaviour
{
    public List<GameObject> toggleObjects;
    public TrapCollider trapCollider;

    public void TrapDisable()
    {
        foreach(GameObject item in toggleObjects)
        {
            item.SetActive(false);
        }
    }

    public void TrapEnable()
    {
        foreach(GameObject item in toggleObjects)
        {
            item.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapToggle : MonoBehaviour
{
    public List<GameObject> toggleObjects;
    public TrapCollider trapCollider;
    public MeshRenderer trapIndicator;

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
            trapIndicator.materials[0].color = new Color(
                trapCollider.alertGuard ? 1 : 0,
                0,
                trapCollider.damage ? 1 : 0,
                0.5f
            );
        }
    }
}

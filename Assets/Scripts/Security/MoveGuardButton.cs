using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGuardButton : Pressable
{
    private Minimap minimap;

    private void Start()
    {
        minimap = GetComponentInParent<Minimap>();
    }

    override public void Press()
    {
        if (minimap.OverrideGuardTarget(transform.GetSiblingIndex()))
            GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
    }
}

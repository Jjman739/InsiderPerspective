using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pressable : MonoBehaviour
{
    public bool pressed = false;

    public abstract void Press();
}

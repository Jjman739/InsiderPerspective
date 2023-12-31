using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            //failsafe not needed right now
            /*if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    return null;
                }
            }*/
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance = this as T;
    }
}
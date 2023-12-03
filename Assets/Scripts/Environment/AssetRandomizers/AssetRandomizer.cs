using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }
}

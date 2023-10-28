using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed;

    void Update()
    {
        gameObject.transform.Rotate(0, spinSpeed * Time.deltaTime, 0, Space.World);
    }
}

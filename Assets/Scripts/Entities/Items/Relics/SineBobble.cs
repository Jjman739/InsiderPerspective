using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBobble : MonoBehaviour
{
    [SerializeField] private float deltaY;
    [SerializeField] private float bobbleSpeed;
    private float baseY;
    private float netTime;

    void Start()
    {
        baseY = transform.localPosition.y;
        netTime = 0;
    }

    void Update()
    {
        netTime += Time.deltaTime;
        float y = baseY + (deltaY * Mathf.Cos(netTime * bobbleSpeed));
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }
}

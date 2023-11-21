using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPositionRandomizer : MonoBehaviour
{
    public float minZ;
    public float maxZ;
    public float minSize;
    public float maxSize;

    [SerializeField] private Transform wall;

    void Start()
    {
        float size = Random.Range(minSize, maxSize);
        minZ += size / 2;
        maxZ -= size / 2;
        float z = Random.Range(minZ, maxZ);

        wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, size);
        wall.transform.localPosition = new Vector3(wall.transform.localPosition.x, wall.transform.localPosition.y, z);
    }
}

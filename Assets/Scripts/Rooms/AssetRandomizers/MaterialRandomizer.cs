using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRandomizer : MonoBehaviour
{
    public Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().SetMaterials(new List<Material> { materials[Random.Range(0, materials.Length)] });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRandomizer : MonoBehaviour
{

    public Texture2D[] textures;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.SetTexture("_MainTex", textures[Random.Range(0, textures.Length)]);
    }
}

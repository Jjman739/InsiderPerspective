using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ColorInvertLens : MonoBehaviour
{
    public bool invert = false;
    [Range(0.0f, 1.0f)]
    public float red = 1.0f;
    [Range(0.0f, 1.0f)]
    public float green = 1.0f;
    [Range(0.0f, 1.0f)]
    public float blue = 1.0f;

    private Material baseMaterial;

    public void Start()
    {
        baseMaterial = new Material(Shader.Find("Hidden/ColorInvertShader"));

        baseMaterial.SetInteger("_Invert", invert ? 1 : 0);
        baseMaterial.SetFloat("_Red", red);
        baseMaterial.SetFloat("_Green", green);
        baseMaterial.SetFloat("_Blue", blue);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, baseMaterial);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ColorStepper : MonoBehaviour
{
    [Range(2, 256)]
    public int m_steps = 10;

    private Material baseMaterial;

    public void Start()
    {
        ApplyShaderVariables();
    }

    private void OnValidate()
    {
        ApplyShaderVariables();
    }

    public void ApplyShaderVariables()
    {
        baseMaterial = new Material(Shader.Find("Hidden/ColorStepper"));

        baseMaterial.SetInteger("_Steps", m_steps);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, baseMaterial);
    }
}

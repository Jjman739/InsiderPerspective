using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ColorInvertLens : MonoBehaviour
{
    public bool m_invert = false;
    public bool m_blackAndWhite = false;
    [Range(0.0f, 1.0f)]
    public float m_red = 1.0f;
    [Range(0.0f, 1.0f)]
    public float m_green = 1.0f;
    [Range(0.0f, 1.0f)]
    public float m_blue = 1.0f;

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
        baseMaterial = new Material(Shader.Find("Hidden/ColorInvertShader"));

        baseMaterial.SetInteger("_Invert", m_invert ? 1 : 0);
        baseMaterial.SetInteger("_BlackAndWhite", m_blackAndWhite ? 1 : 0);
        baseMaterial.SetFloat("_Red", m_red);
        baseMaterial.SetFloat("_Green", m_green);
        baseMaterial.SetFloat("_Blue", m_blue);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, baseMaterial);
    }
}

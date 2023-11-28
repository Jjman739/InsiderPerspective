using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class BlurLens : MonoBehaviour
{
    [Range(0.0f, 0.1f)]
    public float m_blurSize = 0.01f;

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
        baseMaterial = new Material(Shader.Find("Hidden/BlurShader"));

        baseMaterial.SetFloat("_BlurSize", m_blurSize);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, baseMaterial);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class FlipLens : MonoBehaviour
{
    public bool m_flipHorizontally = false;
    public bool m_flipVertically = false;

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
        baseMaterial = new Material(Shader.Find("Hidden/FlipShader"));

        baseMaterial.SetInteger("_FlipHorizontally", m_flipHorizontally ? 1 : 0);
        baseMaterial.SetInteger("_FlipVertically", m_flipVertically ? 1 : 0);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, baseMaterial);
    }
}

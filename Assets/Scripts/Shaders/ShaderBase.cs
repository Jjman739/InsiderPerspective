using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public abstract class ShaderBase : MonoBehaviour
{
    protected Material baseMaterial;

    public void Start()
    {
        ApplyShaderVariables();
    }

    private void OnValidate()
    {
        ApplyShaderVariables();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, baseMaterial);
    }

    public abstract void ApplyShaderVariables();
}

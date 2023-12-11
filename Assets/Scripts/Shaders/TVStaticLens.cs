using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class TVStaticLens : ShaderBase
{
    [Range(0.001f, 1.0f)]
    public float m_scale = 1.0f;
    [Range(0.0f, 1.0f)]
    public float m_blackPixelChance = 0.8f;
    [Range(0.0f, 1.0f)]
    public float m_opacity = 1.0f;
    public bool m_colorMode = false;
    public override void ApplyShaderVariables()
    {
        baseMaterial = new Material(Shader.Find("Hidden/TVStaticShader"));

        baseMaterial.SetFloat("_Scale", m_scale);
        baseMaterial.SetFloat("_BlackPixelChance", m_blackPixelChance);
        baseMaterial.SetFloat("_Opacity", m_opacity);
        baseMaterial.SetInteger("_ColorMode", m_colorMode ? 1 : 0);
    }
}

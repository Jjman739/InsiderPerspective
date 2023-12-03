using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class DarkSpotLens : ShaderBase
{
    [Range(0.0f, 1.0f)]
    public float m_size = 0.5f;
    [Range(-1.0f, 1.0f)]
    public float m_horizontalLocation = 0.0f;
    [Range(-1.0f, 1.0f)]
    public float m_verticalLocation = 0.0f;

    public override void ApplyShaderVariables()
    {
        baseMaterial = new Material(Shader.Find("Hidden/DarkSpotShader"));

        baseMaterial.SetFloat("_Size", m_size);
        baseMaterial.SetFloat("_HorizontalLocation", m_horizontalLocation);
        baseMaterial.SetFloat("_VerticalLocation", m_verticalLocation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class BlurLens : ShaderBase
{
    [Range(0.0f, 0.01f)]
    public float m_blurSize = 0.004f;

    public override void ApplyShaderVariables()
    {
        baseMaterial = new Material(Shader.Find("Hidden/BlurShader"));

        baseMaterial.SetFloat("_BlurSize", m_blurSize);
    }
}

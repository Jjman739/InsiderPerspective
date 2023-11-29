using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class FishEyeLens : ShaderBase
{
    public float m_power = 1.0f;

    public bool m_excludeOuterPixels = true;

    public override void ApplyShaderVariables()
    {
        baseMaterial = new Material(Shader.Find("Hidden/FishEyeShader"));

        baseMaterial.SetFloat("_BarrelPower", m_power);
        baseMaterial.SetInteger("_ExcludeOuterPixels", m_excludeOuterPixels ? 1 : 0);
    }
}

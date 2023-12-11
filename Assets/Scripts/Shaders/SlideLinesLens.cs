using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SlideLinesLens : ShaderBase
{
    [Range(0.1f, 10.0f)]
    public float m_frequency = 1.0f;
    [Range(0.1f, 10.0f)]
    public float m_speed = 1.0f;
    [Range(0.0f, 360.0f)]
    public float m_angleDegreesCounterClockwise = 0.0f;
    public bool m_trippyMode = false;

    public override void ApplyShaderVariables()
    {
        baseMaterial = new Material(Shader.Find("Hidden/SlideLinesShader"));

        baseMaterial.SetFloat("_Frequency", m_frequency);
        baseMaterial.SetFloat("_Speed", m_speed);
        baseMaterial.SetFloat("_RadiansCounterClockwise", m_angleDegreesCounterClockwise * Mathf.PI / 180.0f);
        baseMaterial.SetInteger("_TrippyMode", m_trippyMode ? 1 : 0);
    }
}

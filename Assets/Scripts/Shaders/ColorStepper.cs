using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ColorStepper : ShaderBase
{
    [Range(2, 256)]
    public int m_steps = 10;

    public override void ApplyShaderVariables()
    {
        baseMaterial = new Material(Shader.Find("Hidden/ColorStepper"));

        baseMaterial.SetInteger("_Steps", m_steps);
    }
}

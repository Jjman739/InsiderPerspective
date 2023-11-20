using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class FishEyeLens : MonoBehaviour
{
    public float m_power = 1.0f;

    public bool m_excludeOuterPixels = true;

    private Material baseMaterial;

    public void Start()
    {
        baseMaterial = new Material(Shader.Find("Hidden/FishEyeShader"));

        baseMaterial.SetFloat("_BarrelPower", m_power);
        baseMaterial.SetInteger("_ExcludeOuterPixels", m_excludeOuterPixels ? 1 : 0);
    }

    private void OnValidate() {
        baseMaterial.SetFloat("_BarrelPower", m_power);
        baseMaterial.SetInteger("_ExcludeOuterPixels", m_excludeOuterPixels ? 1 : 0);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, baseMaterial);
    }
}

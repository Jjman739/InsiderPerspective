using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class FishEyeLens : MonoBehaviour
{
    public float pow = 1.0f;

    private Material baseMaterial;

    public void Start()
    {
        baseMaterial = new Material(Shader.Find("Hidden/FishEyeShader"));

        baseMaterial.SetFloat("_BarrelPower", pow);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, baseMaterial);
    }
}

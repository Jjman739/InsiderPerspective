using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaderRandomizer : MonoBehaviour
{  
    private List<Type> easyShaders = new List<Type>
    {
        typeof(BlurLens),
        typeof(ColorInvertLens),
        typeof(ColorStepper),
        typeof(FishEyeLens)
    };
    
    private List<Type> hardShaders = new List<Type>
    {
        typeof(DarkSpotLens),
        typeof(FlipLens)
    };

    private TileRoomModifiers tileRoomModifiers;

    public void Start()
    {
        tileRoomModifiers = transform.parent.parent.GetComponent<TileRoomModifiers>();

        if (tileRoomModifiers is null) return;

        Debug.Log("tile room modifiers is not null");

        for (int i = 0; i < tileRoomModifiers.GetModifierLevelByType(typeof(TileRoomEasyShaders)); i++)
        {
            Debug.Log("spending easy point");

            Type chosenShader = easyShaders[UnityEngine.Random.Range(0, easyShaders.Count)];

            if (gameObject.GetComponent(chosenShader) is null)
            {
                Debug.Log($"shader null, adding component {chosenShader}");
                gameObject.AddComponent(chosenShader);
            }

            randomizeShaderValues(GetComponent(chosenShader) as ShaderBase);
        }

        for (int i = 0; i < tileRoomModifiers.GetModifierLevelByType(typeof(TileRoomHardShaders)); i++)
        {
            Type chosenShader = hardShaders[UnityEngine.Random.Range(0, hardShaders.Count)];

            if (GetComponent(chosenShader) is null)
            {
                gameObject.AddComponent(chosenShader);
            }

            randomizeShaderValues(GetComponent(chosenShader) as ShaderBase);
        }
    }

    private void randomizeShaderValues(ShaderBase shader)
    {
        if (shader is BlurLens)
        {
            BlurLens blurLens = shader as BlurLens;
            blurLens.m_blurSize += 0.05f * (UnityEngine.Random.Range(0,2) == 1 ? 1 : -1);
        }

        else if (shader is ColorInvertLens)
        {
            ColorInvertLens colorInvertLens = shader as ColorInvertLens;
            int random = UnityEngine.Random.Range(0,6);
            if (random < 3)
            {
                float decrement = 0.2f;
                switch(UnityEngine.Random.Range(0,3))
                {
                    case 0:
                        colorInvertLens.m_red -= decrement;
                        break;

                    case 1:
                        colorInvertLens.m_blue -= decrement;
                        break;

                    case 2:
                        colorInvertLens.m_green -= decrement;
                        break;
                }
            }
            else if (random >= 3 && random < 5)
            {
                colorInvertLens.m_blackAndWhite = !colorInvertLens.m_blackAndWhite;
            }
            else if (random >= 5)
            {
                colorInvertLens.m_invert = !colorInvertLens.m_invert;
            }
        }

        else if (shader is ColorStepper)
        {
            ColorStepper colorStepper = shader as ColorStepper;
            colorStepper.m_steps = UnityEngine.Random.Range(2, 256);
        }

        else if (shader is DarkSpotLens)
        {
            DarkSpotLens darkSpotLens = shader as DarkSpotLens;
            darkSpotLens.m_size += 0.1f;
            darkSpotLens.m_horizontalLocation = UnityEngine.Random.Range(-1.0f, 1.0f);
            darkSpotLens.m_verticalLocation = UnityEngine.Random.Range(-1.0f, 1.0f);
        }

        else if (shader is FishEyeLens)
        {
            FishEyeLens fishEyeLens = shader as FishEyeLens;
            fishEyeLens.m_power += 1.0f;
            fishEyeLens.m_excludeOuterPixels = UnityEngine.Random.Range(0,2) == 1;
        }

        else if (shader is FlipLens)
        {
            FlipLens flipLens = shader as FlipLens;
            flipLens.m_flipHorizontally = UnityEngine.Random.Range(0,2) == 1;
            flipLens.m_flipVertically = UnityEngine.Random.Range(0,2) == 1;
        }

        shader.ApplyShaderVariables();
    }
}

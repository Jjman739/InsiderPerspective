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

    private List<Type> allShaders = new List<Type>
    {
        typeof(BlurLens),
        typeof(ColorInvertLens),
        typeof(ColorStepper),
        typeof(FishEyeLens),
        typeof(DarkSpotLens),
        typeof(FlipLens)
    };

    private TileRoomModifiers tileRoomModifiers;

    public void Start()
    {
        tileRoomModifiers = transform.parent.GetComponent<TileRoomModifiers>();

        if (tileRoomModifiers is null) return;

        for (int i = 0; i < tileRoomModifiers.GetModifierLevelByType(typeof(TileRoomEasyShaders)); i++)
        {
            Type chosenShader = easyShaders[UnityEngine.Random.Range(0, easyShaders.Count)];

            GameObject camera = transform.GetChild(UnityEngine.Random.Range(0, transform.childCount)).gameObject;


            if (camera.GetComponent(chosenShader) is null)
            {
                Debug.Log("adding component");
                camera.AddComponent(chosenShader);
            }

            randomizeShaderValues(camera.GetComponent(chosenShader) as ShaderBase);
        }

        for (int i = 0; i < tileRoomModifiers.GetModifierLevelByType(typeof(TileRoomHardShaders)); i++)
        {
            Type chosenShader = hardShaders[UnityEngine.Random.Range(0, hardShaders.Count)];

            GameObject camera = transform.GetChild(UnityEngine.Random.Range(0, transform.childCount)).gameObject;

            if (camera.GetComponent(chosenShader) is null)
            {
                camera.AddComponent(chosenShader);
            }

            randomizeShaderValues(camera.GetComponent(chosenShader) as ShaderBase);
        }
    }

    private void randomizeShaderValues(ShaderBase shader)
    {
        if (shader is BlurLens)
        {
            BlurLens blurLens = shader as BlurLens;
            blurLens.m_blurSize += 0.01f * (UnityEngine.Random.Range(0,2) == 1 ? 1 : -1);
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

    public GameObject getCurrentCamera()
    {
        if (CameraViewer.Instance.GetCurrentCameraGroup() == transform)
        {
            Debug.Log("Current camera found.");
            return CameraViewer.Instance.GetCamera().gameObject;
        }

        foreach (Transform cameraTransform in transform)
        {
            Camera camera = cameraTransform.gameObject.GetComponent<Camera>();
            if (camera.enabled)
            {
                return cameraTransform.gameObject;
            }
        }

        // If no camera is active, something is very wrong, but choose the first one.
        Debug.Log("No camera active in room!");
        return transform.GetChild(0).gameObject;
    }

    public void applyRandomShader()
    {
        GameObject camera = getCurrentCamera();
        Type chosenShader = allShaders[UnityEngine.Random.Range(0, allShaders.Count)];

        if (camera.GetComponent(chosenShader) is null)
        {
            Debug.Log("adding component");
            camera.AddComponent(chosenShader);
            randomizeShaderValues(camera.GetComponent(chosenShader) as ShaderBase);
        }
    }
}
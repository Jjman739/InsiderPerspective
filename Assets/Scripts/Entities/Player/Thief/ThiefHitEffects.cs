using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefHitEffects : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameraTurners;
    [SerializeField] private List<GameObject> zapEffects;

    private float hitTimeRemaining = 0;
    private float cameraTurnSpeed;

    public void TakeHit()
    {
        hitTimeRemaining = 1f;
        cameraTurnSpeed = Random.Range(1080f, 1440f);
        foreach (GameObject effect in zapEffects)
        {
            effect.SetActive(true);
        }
    }

    private void HitEnd()
    {
        foreach (GameObject effect in zapEffects)
        {
            effect.SetActive(false);
        }
    }

    void Update()
    {
        if (hitTimeRemaining <= 0)
        {
            return;
        }

        hitTimeRemaining -= Time.deltaTime;
        if (hitTimeRemaining <= 0)
        {
            HitEnd();
        } else
        {
            foreach (GameObject camera in cameraTurners)
            {
                Quaternion rotation = camera.transform.localRotation;
                rotation.eulerAngles = new Vector3(
                    rotation.eulerAngles.x,
                    rotation.eulerAngles.y + (cameraTurnSpeed * Time.deltaTime),
                    rotation.eulerAngles.z);
                camera.transform.localRotation = rotation;
            }
            cameraTurnSpeed -= 900f * Time.deltaTime;
        }
    }

    public void Repair()
    {
        foreach (GameObject camera in cameraTurners)
        {
            camera.transform.localRotation = Quaternion.identity;
        }
    }
}

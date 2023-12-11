using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefHitEffects : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameraTurners;
    [SerializeField] private List<GameObject> zapEffects;
    [SerializeField] private float pitchExtent = 20;
    [SerializeField] private float pitchCenter = 0;
    [SerializeField] private float rollExtent = 40;
    [SerializeField] private float rollCenter = 0;

    private float hitTimeRemaining = 0;
    private float cameraTurnSpeed;
    private float pitchSpeed;
    private float rollSpeed;
    private float pitchTimer = 0;
    private float rollTimer = 0;

    public void TakeHit()
    {
        hitTimeRemaining = 1f;
        cameraTurnSpeed = GameManager.RNG.Next(1080, 1440);
        pitchSpeed = 0.01f * GameManager.RNG.Next(500, 2000);
        rollSpeed = 0.01f * GameManager.RNG.Next(500, 2000);

        GameObject sampleTurner = cameraTurners[0];

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
            pitchTimer += Time.deltaTime * pitchSpeed;
            rollTimer += Time.deltaTime * rollSpeed;
            float newPitch = pitchCenter + (pitchExtent * Mathf.Sin(pitchTimer));
            float newRoll = rollCenter + (rollExtent * Mathf.Sin(rollTimer));
            foreach (GameObject camera in cameraTurners)
            {
                Quaternion rotation = camera.transform.localRotation;
                rotation.eulerAngles = new Vector3(
                    newPitch,
                    rotation.eulerAngles.y + (cameraTurnSpeed * Time.deltaTime),
                    newRoll);
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

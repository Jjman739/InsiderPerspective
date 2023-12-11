using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enumerations;

public class ThiefManager : MonoBehaviour
{
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private ThiefPhoto photo;
    [SerializeField] private ThiefMovementScript movement;
    [SerializeField] private ThiefTreasure treasure;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip alarmSound;
    [SerializeField] private ThiefHitEffects hitEffects;
    [SerializeField] private ParticleSystem shockEffect;
    

    private AudioSource zapAudioSource;
    private AudioSource alarmAudioSource;

    public int repairsRemaining = 3;
    public bool needsRepair = false;

    private void Start()
    {
        shockEffect.Stop();
        zapAudioSource = GetComponents<AudioSource>()[1];
        alarmAudioSource = GetComponents<AudioSource>()[2];
    }

    public void TakeDamage()
    {
        hitEffects.TakeHit();
        ScrambleControls(ref movement.forwardButton, ref movement.backwardButton, ref movement.leftButton, ref movement.rightButton, ref movement.jumpButton, ref photo.photoButton);
        needsRepair = true;
        shockEffect.Play();

        PlayHurtSound();
        DialogueManager.Instance.PlayDialogue(DialogueEvent.TRAP_SHOCK);
    }

    public void PlayHurtSound()
    {
        zapAudioSource.Play();
    }

    public void PlayAlarmSound()
    {
        alarmAudioSource.Play();
    }

    public bool AttemptWin()
    {
        if (treasure.treasureCount >= treasure.goal)
        {
            SceneManager.LoadScene("WinScene");
            return true;
        }
        return false;
    }

    public void Repair()
    {
        if (GameManager.Instance.GetCurrentGameMode() == GameMode.SURVIVAL)
        {
            movement.chargeTimer = movement.maxChargeTimer;
            return;
        }

        hitEffects.Repair();

        movement.forwardButton = "ThiefMoveUp";
        movement.backwardButton = "ThiefMoveDown";
        movement.leftButton = "ThiefMoveLeft";
        movement.rightButton = "ThiefMoveRight";
        movement.jumpButton = "Jump";
        photo.photoButton = "ThiefPhoto";

        GameObject cameraObject = GetCameraObject();
        foreach (ShaderBase shader in cameraObject.GetComponents<ShaderBase>())
        {
            Destroy(shader);
        }

        repairsRemaining--;
        needsRepair = false;
    }

    private void ScrambleControls(ref string forward, ref string backward, ref string left, ref string right, ref string jump, ref string cam)
    {
        int buttonId1 = Random.Range(0,5);
        int buttonId2 = Random.Range(0,4);
        if (buttonId2 == buttonId1)
        {
            buttonId2++;
        }

        List<string> values = new List<string> { forward, backward, left, right, jump, cam };
        string temp = values[buttonId1];
        values[buttonId1] = values[buttonId2];
        values[buttonId2] = temp;

        forward = values[0];
        backward = values[1];
        left = values[2];
        right = values[3];
        jump = values[4];
        cam = values[5];
    }

    public List<string> ReturnControls()
    {
        string forward = movement.forwardButton;
        string back = movement.backwardButton;
        string left = movement.leftButton;
        string right = movement.rightButton;
        string jump = movement.jumpButton;
        string cam = photo.photoButton;

        List<string> controls = new List<string> { forward, back, left, right, jump, cam };
        return controls;
    }

    public GameObject GetCameraObject()
    {
        return cameraRoot.transform.GetChild(0).gameObject;
    }

    public void Stun(float duration)
    {
        movement.stunTimer = duration;
    }
}

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
    [SerializeField] private ThiefHitEffects hitEffects;
    private AudioSource audioSource;

    public int repairsRemaining = 3;
    public bool needsRepair = false;

    private void Start()
    {
        audioSource = GetComponents<AudioSource>()[1];
    }

    public void TakeDamage()
    {
        Debug.Log("Hit a trap.");
        hitEffects.TakeHit();
        ScrambleControls(ref movement.forwardButton, ref movement.backwardButton, ref movement.leftButton, ref movement.rightButton, ref movement.jumpButton, ref photo.photoButton);
        needsRepair = true;
        audioSource.clip = hurtSound;
        audioSource.loop = false;
        audioSource.Play();
        DialogueManager.Instance.PlayDialogue(DialogueEvent.SHOCK_ROBOT);
    }

    public bool AttemptWin()
    {
        if (treasure.treasureCount >= treasure.goal)
        {
            Debug.Log("Win!");
            SceneManager.LoadScene("WinScene");
            return true;
        }
        return false;
    }

    public void Repair()
    {
        Debug.Log("Repairing!");

        hitEffects.Repair();
        movement.forwardButton = "ThiefMoveUp";
        movement.backwardButton = "ThiefMoveDown";
        movement.leftButton = "ThiefMoveLeft";
        movement.rightButton = "ThiefMoveRight";
        movement.jumpButton = "Jump";
        photo.photoButton = "ThiefPhoto";
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
}

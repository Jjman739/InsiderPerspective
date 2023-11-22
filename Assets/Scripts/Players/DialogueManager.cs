using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Enumerations;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private AudioClip gameStart;
    [SerializeField] private AudioClip viewLaptop;
    [SerializeField] private AudioClip viewMonitor;
    [SerializeField] private AudioClip viewFishEyeMonitor;
    [SerializeField] private AudioClip guardTileRoom;
    [SerializeField] private AudioClip shockTileRoom;
    [SerializeField] private AudioClip shockRobot;
    [SerializeField] private AudioClip platformerRoom;
    [SerializeField] private AudioClip gameEnd;
    [SerializeField] private GameObject skipDialogueText;

    private AudioSource audioSource;
    private List<DialogueObject> dialogueObjects = new();
    private DialogueObject currentDialogue;

    private new void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();

        initializeDialogueObjects();
    }

    private void initializeDialogueObjects()
    {
        dialogueObjects.Add(new DialogueObject(gameStart, DialogueEvent.GAME_START));
        dialogueObjects.Add(new DialogueObject(viewLaptop, DialogueEvent.VIEW_LAPTOP));
        dialogueObjects.Add(new DialogueObject(viewMonitor, DialogueEvent.VIEW_MONITOR));
        dialogueObjects.Add(new DialogueObject(viewFishEyeMonitor, DialogueEvent.VIEW_MONITOR_FISH_EYE));
        dialogueObjects.Add(new DialogueObject(guardTileRoom, DialogueEvent.ENTER_TILE_ROOM_GUARD));
        dialogueObjects.Add(new DialogueObject(shockTileRoom, DialogueEvent.ENTER_TILE_ROOM_SHOCK));
        dialogueObjects.Add(new DialogueObject(shockRobot, DialogueEvent.SHOCK_ROBOT));
        dialogueObjects.Add(new DialogueObject(platformerRoom, DialogueEvent.PLATFORMER_ROOM));
        dialogueObjects.Add(new DialogueObject(gameEnd, DialogueEvent.GAME_END));
    }

    private void Update()
    {
        skipDialogueText.SetActive(audioSource.isPlaying);

        if (!audioSource.isPlaying)
        {
            currentDialogue = null;
        }

        if ((Input.GetButton("SkipDialogue") || Time.timeScale == 0) && currentDialogue is not null)
        {
            currentDialogue.Stop(audioSource);
        }
    }

    public void PlayDialogue(DialogueEvent dialogueEvent)
    {
        DialogueObject dialogueToPlay = getDialogueObjectByEvent(dialogueEvent);

        if (dialogueToPlay is null || dialogueToPlay.HasPlayed()) return;

        if (currentDialogue is not null)
        {
            currentDialogue.Stop(audioSource);
        }

        dialogueToPlay.Play(audioSource);
        currentDialogue = dialogueToPlay;
    }

    private DialogueObject getDialogueObjectByEvent(DialogueEvent dialogueEvent)
    {
        foreach (DialogueObject dialogueObject in dialogueObjects)
        {
            if (dialogueObject.GetDialogueEvent() == dialogueEvent)
            {
                return dialogueObject;
            }
        }

        return null;
    }
}

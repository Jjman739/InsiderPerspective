using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Enumerations;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private AudioClip gameStart;
    [SerializeField] private AudioClip moveRobot;
    [SerializeField] private AudioClip viewStartRoom;
    [SerializeField] private AudioClip viewHallway;
    [SerializeField] private AudioClip viewTileRoom;
    [SerializeField] private AudioClip viewPlatformerRoom;
    [SerializeField] private AudioClip trapShock;
    [SerializeField] private AudioClip trapGuard;
    [SerializeField] private AudioClip trapCamera;
    [SerializeField] private AudioClip repairKit;
    [SerializeField] private AudioClip relic;
    [SerializeField] private AudioClip allRelics;
    [SerializeField] private AudioClip gameEndLose;
    [SerializeField] private AudioClip gameEndWin;
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
        dialogueObjects.Add(new DialogueObject(moveRobot, DialogueEvent.MOVE_ROBOT));
        dialogueObjects.Add(new DialogueObject(viewStartRoom, DialogueEvent.VIEW_START_ROOM));
        dialogueObjects.Add(new DialogueObject(viewHallway, DialogueEvent.VIEW_HALLWAY));
        dialogueObjects.Add(new DialogueObject(viewTileRoom, DialogueEvent.VIEW_TILE_ROOM));
        dialogueObjects.Add(new DialogueObject(viewPlatformerRoom, DialogueEvent.VIEW_PLATFORMER_ROOM));
        dialogueObjects.Add(new DialogueObject(trapShock, DialogueEvent.TRAP_SHOCK));
        dialogueObjects.Add(new DialogueObject(trapGuard, DialogueEvent.TRAP_GUARD));
        dialogueObjects.Add(new DialogueObject(trapCamera, DialogueEvent.TRAP_CAMERA));
        dialogueObjects.Add(new DialogueObject(repairKit, DialogueEvent.REPAIR_KIT));
        dialogueObjects.Add(new DialogueObject(relic, DialogueEvent.RELIC));
        dialogueObjects.Add(new DialogueObject(allRelics, DialogueEvent.ALL_RELICS));
        dialogueObjects.Add(new DialogueObject(gameEndLose, DialogueEvent.GAME_END_LOSE));
        dialogueObjects.Add(new DialogueObject(gameEndWin, DialogueEvent.GAME_END_WIN));
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

        //Uncomment this if you need to test dialogue events but cannot listen to the result because you are in class
        //Debug.Log($"Now playing {dialogueEvent}...");
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

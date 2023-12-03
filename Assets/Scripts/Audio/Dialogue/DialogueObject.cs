using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerations;

public class DialogueObject
{
    private AudioClip audioClip;
    private DialogueEvent dialogueEvent;
    private bool played;

    public DialogueObject(AudioClip audioClip, DialogueEvent dialogueEvent)
    {
        this.audioClip = audioClip;
        this.dialogueEvent = dialogueEvent;
    }

    public void Play(AudioSource audioSource)
    {
        if (played) return;

        audioSource.clip = audioClip;
        audioSource.Play();
        played = true;
    }

    public void Stop(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public AudioClip GetAudioClip() { return audioClip; }
    public DialogueEvent GetDialogueEvent() { return dialogueEvent; }
    public bool HasPlayed() { return played; }
}

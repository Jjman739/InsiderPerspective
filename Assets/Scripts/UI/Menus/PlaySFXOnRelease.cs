using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySFXOnRelease : MonoBehaviour, IPointerUpHandler
{
    AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerUp(PointerEventData d)
    {
        audioSource.Play();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHoldReleaseHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onPointerDown;
    public UnityEvent onPointerUp;
    private Button button;

    public void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!button.interactable) return;

        onPointerDown?.Invoke();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUp?.Invoke();
    }
}

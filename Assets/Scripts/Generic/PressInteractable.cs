using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressInteractable : MonoBehaviour, ISuccessTrigger
{
    [SerializeField] private KeyCode toPress;

    public event Action OnSuccess;

    private bool eventTriggered;

    void Update()
    {
        if (!eventTriggered && Input.GetKey(toPress))
        {
            OnSuccess?.Invoke();
            eventTriggered = true;
        }
    }
    public void SetPress(KeyCode key)
    {
        toPress = key;
    }
    
    public void ResetState()
    {
        eventTriggered = false;
    }
}

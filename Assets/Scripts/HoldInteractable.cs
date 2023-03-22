using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldInteractable : MonoBehaviour, IProgressProvider
{
    [SerializeField] private float holdTime = 3f;
    [SerializeField] private KeyCode toHold;

    public event Action OnSuccess;

    private float currentHoldTime = 0f;
    private bool eventTriggered;

    void Update()
    {
        if (Input.GetKey(toHold))
            currentHoldTime += Time.deltaTime;
        else
        {
            eventTriggered = false;
            currentHoldTime = 0f;
        }

        if (!eventTriggered && currentHoldTime >= holdTime)
            OnSuccess?.Invoke();
    }

    public float Progress()
    {
        return currentHoldTime / holdTime;
    }

    public void SetHold(KeyCode k)
    {
        toHold = k;
    }

    public void ResetState()
    {
        currentHoldTime = 0f;
    }
}

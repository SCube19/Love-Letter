using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldInteractable : MonoBehaviour, IProgressProvider
{
    [SerializeField] private float holdTime = 3f;
    [SerializeField] private KeyCode toHold;
    [SerializeField] private AudioSource holdSound;

    public event Action OnSuccess;

    private float currentHoldTime = 0f;
    private bool eventTriggered;

    void Update()
    {
        if (Input.GetKey(toHold))
        {
            currentHoldTime += Time.deltaTime;
            if (!holdSound.isPlaying)
                holdSound.Play();
        }
        else
        {
            eventTriggered = false;
            currentHoldTime = 0f;
            holdSound.Stop();
        }

        if (!eventTriggered && currentHoldTime >= holdTime) { 
            OnSuccess?.Invoke();
            holdSound.Stop();
        }
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

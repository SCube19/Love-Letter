using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEvent : MonoBehaviour, IProgressProvider
{
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private bool startPassed = false;

    public event Action OnTimePassed;

    private float timePassed = 0f;
    private bool eventTriggered;

    void Start()
    {
        if (startPassed)
            timePassed = waitTime;
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if (!eventTriggered && timePassed >= waitTime)
            OnTimePassed?.Invoke();
    }

    public void SetWaitTime(float waitTime)
    {
        this.waitTime = waitTime;
    }

    public float Progress()
    {
        return timePassed / waitTime;
    }

    public void ResetState()
    {
        eventTriggered = false;
        timePassed = 0f;
    }
}

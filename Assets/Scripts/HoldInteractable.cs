using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldInteractable : MonoBehaviour
{
    [SerializeField] private float holdTime = 3f;
    [SerializeField] private KeyCode toHold;

    public event Action OnHoldSuccess;

    private float currentHoldTime = 0f;

    void Update()
    {
        if (Input.GetKey(toHold))
            currentHoldTime += Time.deltaTime;
        else
            currentHoldTime = 0f;

        if (currentHoldTime >= holdTime)
            OnHoldSuccess.Invoke();
    }

    public float Progress()
    {
        return currentHoldTime / holdTime;
    }
}

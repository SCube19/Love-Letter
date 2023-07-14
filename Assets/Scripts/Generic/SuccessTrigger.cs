using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessTrigger : MonoBehaviour
{
    public event Action OnSuccess;
    public void Success()
    {
        OnSuccess?.Invoke();
    }
}

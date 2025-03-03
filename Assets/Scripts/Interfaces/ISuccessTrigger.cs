using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISuccessTrigger 
{
    public event Action OnSuccess;

    public void ResetState();
}

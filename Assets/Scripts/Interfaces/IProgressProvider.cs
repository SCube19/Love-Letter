using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IProgressProvider
{
    public float Progress();
    public void ResetProgress();
}

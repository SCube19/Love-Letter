using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager
{
    public enum Controls 
    {
        None,
        MoveRight,
        MoveLeft,
        Jump,
        Duck,
        Dash,
        Attack,
        Interact
    }


    private Tuple<Controls, KeyCode>[] defaultControls = {
        new Tuple<Controls, KeyCode>(Controls.MoveRight, KeyCode.RightArrow),
        new Tuple<Controls, KeyCode>(Controls.MoveLeft, KeyCode.LeftArrow),
        new Tuple<Controls, KeyCode>(Controls.Jump, KeyCode.UpArrow),
        new Tuple<Controls, KeyCode>(Controls.Duck, KeyCode.DownArrow),
        new Tuple<Controls, KeyCode>(Controls.Dash, KeyCode.X),
        new Tuple<Controls, KeyCode>(Controls.Attack, KeyCode.C),
        new Tuple<Controls, KeyCode>(Controls.Interact, KeyCode.F)
    };

    public Dictionary<Controls, KeyCode> ControlMap { get; }

    private Dictionary<KeyCode, Controls> reverseMap;
    
    public ControlsManager() 
    { 
        ControlMap = new Dictionary<Controls, KeyCode>();
        reverseMap = new Dictionary<KeyCode, Controls>();

        foreach (var (control, key) in defaultControls)
        {
            ControlMap[control] = key;
            reverseMap[key] = control;
        }
    }

    private static ControlsManager _instance;

    private static readonly object _lock = new object();

    public static ControlsManager GetInstance()
    {
        if (_instance == null)
            lock (_lock)
                _instance ??= new ControlsManager();

        return _instance;
    }

    public void SetControl(Controls control, KeyCode key)
    {
        ControlMap[control] = key;

        Controls old = reverseMap.GetValueOrDefault(key, Controls.None);

        ControlMap[old] = KeyCode.None; 
        reverseMap[key] = control;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager
{
    public int ChamberId { get; set; }
    public SceneManager() { }

    private static SceneManager _instance;

    private static readonly object _lock = new object();

    public static SceneManager GetInstance()
    {
        if (_instance == null)
            lock (_lock)
                _instance ??= new SceneManager();
           
        return _instance;
    }
}

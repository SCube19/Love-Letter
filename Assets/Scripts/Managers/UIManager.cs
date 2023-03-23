using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TimedEvent dashTimer;
    [SerializeField] private 
    private float _dashCooldown;
    public float DashCooldown
    {
        get => _dashCooldown;
        set { _dashCooldown = value; dashTimer.SetWaitTime(value); } 
    }

    public void Awake()
    {
        DashCooldown = player.GetComponent<PlayerMovement>().DashCooldown;
        player.GetComponent<PlayerMovement>().OnDash += ResetDashTimer;
    }

    public void OnDestroy()
    {
        player.GetComponent<PlayerMovement>().OnDash -= ResetDashTimer;
    }

    public void ResetDashTimer()
    {
        dashTimer.ResetState();
    }

}

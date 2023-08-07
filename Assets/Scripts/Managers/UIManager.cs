using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TimedEvent dashTimer;
    [SerializeField] private GameObject heartSet;

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
        player.GetComponentInChildren<PlayerPickupController>().OnHeartPickup += LightUpHeartFragment;
        player.GetComponentInChildren<PlayerPickupController>().OnHeartTaken += FadeHeartFragment;
    }

    public void OnDestroy()
    {
        player.GetComponent<PlayerMovement>().OnDash -= ResetDashTimer;
        player.GetComponentInChildren<PlayerPickupController>().OnHeartPickup -= LightUpHeartFragment;
        player.GetComponentInChildren<PlayerPickupController>().OnHeartTaken -= FadeHeartFragment;
    }

    public void ResetDashTimer()
    {
        dashTimer.ResetProgress();
    }

    private void LightUpHeartFragment(Heart heart)
    {
        heartSet.transform.Find($"heart_pieces_{heart.Index}").gameObject.SetActive(true);
    }

    private void FadeHeartFragment(Heart heart)
    {
        heartSet.transform.Find($"heart_pieces_{heart.Index}").gameObject.SetActive(false);
    }

}

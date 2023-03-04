using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private bool IsHeart;

    public event Action OnPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        
        if (IsHeart)
        {
            Debug.Log("Heart Pickup!!");
        }
        OnPickup?.Invoke();
    }
}

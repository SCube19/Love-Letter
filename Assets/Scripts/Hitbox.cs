using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private List<string> detectTags;
    [SerializeField] private Invincibility invincibilitySource;

    public event Action OnHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!detectTags.Exists(tag => collision.CompareTag(tag))
            || invincibilitySource.IsInvincible)
            return;

        Debug.Log("hit");
        OnHit?.Invoke();
    }
}

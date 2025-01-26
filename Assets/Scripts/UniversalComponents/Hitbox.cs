using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private List<string> detectTags;

    public event Action<Collider2D> OnHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!detectTags.Exists(tag => collision.CompareTag(tag)))
            return;

        OnHit?.Invoke(collision);
    }
}

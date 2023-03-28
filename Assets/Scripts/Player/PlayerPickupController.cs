using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerPickupController : MonoBehaviour
{
    public List<Heart> Hearts { get; private set; }

    public event Action<Heart> OnHeartPickup;
    public event Action<Heart> OnHeartTaken;

    private void Awake()
    {
        Hearts = new List<Heart>();  
    }
    
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Pickup"))
            collider.gameObject.GetComponent<Pickup>().Affect(gameObject);
    }

    public void CollectHeart(Heart heart)
    {
        Hearts.Add(heart);
        OnHeartPickup?.Invoke(heart);
    }

    public Heart TakeHeart(int id)
    {
        int heartId = Hearts.FindIndex(el => el.Index == id);
        Heart heart = Hearts[heartId];
        Hearts.RemoveAt(heartId);
        OnHeartTaken?.Invoke(heart);
        return heart;
    }

    public Heart TakeHeartRandom()
    {
        int heartId = UnityEngine.Random.Range(0, Hearts.Count - 1);
        Heart heart = Hearts[heartId];
        Hearts.RemoveAt(heartId);
        OnHeartTaken?.Invoke(heart);
        return heart;
    }
}

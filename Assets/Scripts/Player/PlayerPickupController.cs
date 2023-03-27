using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupController : MonoBehaviour
{
    public List<Heart> Hearts { get; private set; }

    public event Action<Heart> OnHeartPickup;
    public event Action<Heart> OnHeartTaken;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        IPickup pickup = collision.gameObject.GetComponent<IPickup>();
        pickup?.Affect(gameObject);
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

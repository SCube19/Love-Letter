using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, IPickup
{
    [SerializeField] private int _index;
    public int Index
    {
        get { return _index; }
        set { _index = value; }
    }

    public string LetterFragment { get; private set; }

    public Sprite HeartTexture
    {
        get { return Resources.LoadAll<Sprite>($"SpriteSheets/heart_pieces")[_index]; }
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = HeartTexture;
    }

    public void Affect(GameObject player)
    {
        player.GetComponent<PlayerPickupController>().CollectHeart(this);
        Destroy(gameObject);
    }
}

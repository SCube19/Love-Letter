using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Pickup
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

    public override void Affect(GameObject player)
    {
        player.GetComponentInChildren<PlayerPickupController>().CollectHeart(GetComponent<Heart>());
        PlaySound();
        GetComponent<Animator>().SetTrigger("PickedUp");
    }

    public void AnimEnd()
    {
        Destroy(transform.parent.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: rewrite so it's not it's own class or entity across the project. Pickups can have effects so use it
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

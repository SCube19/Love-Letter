using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class AttackReceiver : MonoBehaviour
{
    [SerializeField] public int Hp { get; set; }
    public virtual void Damage(int amount)
    {
        this.Hp -= amount;
    }

    public virtual void Launch(float force)
    {
        if (this.GetComponent<Rigidbody2D>() != null)
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, force), ForceMode2D.Impulse);
    }
}

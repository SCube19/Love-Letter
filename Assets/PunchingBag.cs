using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingBag : AttackReceiver
{
    public override void Launch(float force)
    {
        base.Launch(force);
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }
}

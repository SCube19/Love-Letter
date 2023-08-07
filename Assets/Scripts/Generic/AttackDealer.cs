using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//probably should be scriptable object
public abstract class AttackDealer: MonoBehaviour
{
    public bool CanAttack { get; protected set; }

    public abstract void Attack(AttackReceiver target);

    public abstract void AttackAnimation();
    public IEnumerator StartCooldown(float cooldown)
    {
        this.CanAttack = false;
        yield return new WaitForSecondsRealtime(cooldown);
        this.CanAttack = true;
    }
}

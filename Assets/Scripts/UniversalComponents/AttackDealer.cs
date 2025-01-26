using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Dependency Injection
public abstract class AttackDealer: MonoBehaviour
{
    public bool CanAttack { get; protected set; }

    public AttackObject CurrentAttack { get; protected set; }

    public abstract void Attack(AttackReceiver target);

    public abstract void AttackAnimation();
    private IEnumerator _StartCooldown(float cooldown)
    {
        yield return new WaitForSecondsRealtime(cooldown);
        this.CanAttack = true;
    }

    public void StartAttack(AttackObject attack)
    {
        this.CanAttack = false;
        this.CurrentAttack = attack;
        StartCoroutine(_StartCooldown(attack.cooldown));
    }
}

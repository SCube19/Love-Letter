using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackDealer
{
    public AttackObject standAttack;
    public AttackObject moveAttack;
    public AttackObject airAttack;

    // Start is called before the first frame update
    void Awake()
    {
        this.transform.Find("AttackBox").GetComponent<Hitbox>().OnHit +=
            collider => { this.Attack(collider.GetComponent<AttackReceiver>()); };

        this.CanAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        AttackAnimation();
    }

    public override void AttackAnimation()
    {
        if (!this.CanAttack)
            return;
        if (Input.GetKeyDown(ControlsManager.GetInstance().ControlMap[ControlsManager.Controls.Attack]))
        {
            GetComponent<Animator>().SetTrigger("attack");
            this.CanAttack = false;
            StartCoroutine(this.StartCooldown(standAttack.cooldown));
        }
    }

    public void ResetAttackTrigger()
    {
        GetComponent<Animator>().ResetTrigger("attack");
    }

    public override void Attack(AttackReceiver target)
    {
        this.DealStandingAttack(target);
    }

    private void DealStandingAttack(AttackReceiver target)
    {
        target.Damage(standAttack.damage);
        target.Launch(standAttack.launchForce);

    }
}

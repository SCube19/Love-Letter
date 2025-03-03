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
            AttackObject chosenAttack;
           
            if (!GetComponent<PlayerMovement>().IsGrounded())
            {
                chosenAttack = airAttack;
                GetComponent<Animator>().SetTrigger("attackAir");
            }
            else if (GetComponent<PlayerMovement>().IsMoving())
            {
                chosenAttack = moveAttack;
                GetComponent<Animator>().SetTrigger("attackMoving");
            }
            else
            {
                chosenAttack = standAttack;
                GetComponent<Animator>().SetTrigger("attackStanding");
            }
            this.StartAttack(chosenAttack);
        }
    }

    public void ResetAttackTrigger()
    {
        GetComponent<Animator>().ResetTrigger("attack");
    }

    public override void Attack(AttackReceiver target)
    {
        target.Damage(CurrentAttack.damage);
        target.Launch(CurrentAttack.launchForce);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : ISemiBoss
{
    private SemiBoss semiBoss;
    private float attackTimer; //time of previous shot
    private float attackCD = 1.5f; // time for next fire
    public bool canAttack;
    public void Enter(SemiBoss semiBoss)
    {
        this.semiBoss = semiBoss;
    }

    public void Execute()
    {
        Debug.Log("Melee State");
        Attack();
        if (semiBoss.InShootRange && (!semiBoss.InMeleeRange))
        {
            semiBoss.s_anim.SetBool("Melee", false);
            semiBoss.ChangeState(new RangeState());
        }
        else if (semiBoss.Target == null)
        {
            semiBoss.s_anim.SetBool("Melee", false);
            semiBoss.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCD)
        {
            canAttack = true;
            attackTimer = 0;
        }

        if (canAttack)
        {

            canAttack = false;
            semiBoss.s_anim.SetBool("Melee", true);
        }
    }
}

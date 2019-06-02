using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : ISemiBoss
{
    private SemiBoss semiBoss;
    private float attackTimer; //time of previous shot
    private float attackCD = 1.5f; // time for next fire
    private bool canAttack = true;
    public void Enter(SemiBoss semiBoss)
    {
        this.semiBoss = semiBoss;
    }

    public void Execute()
    {
        Debug.Log("meleee");
        Attack();
        if (semiBoss.inShootRange && !semiBoss.inMeleeRange)
        {
            semiBoss.ChangeState(new RangeState());
            semiBoss.s_anim.SetBool("Melee", false);
        }
        if (semiBoss.Target == null)
        {
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
            // semiBoss.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // cai nay test sau
            if (semiBoss.Target.activeSelf)
            {
                semiBoss.MeleeAtk();
            }

            //semiBoss.s_anim.SetFloat("spd", 0);
        }
    }

}

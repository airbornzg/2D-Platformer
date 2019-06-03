using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeState : ISemiBoss
{
    private SemiBoss semiBoss;
    private float shootTimer; //time of previous shot
    private float shootCD = 1.0f; // time for next fire
    public bool canShoot;
    public void Enter(SemiBoss semiBoss)
    {
        this.semiBoss = semiBoss;
    }

    public void Execute()
    {
        Debug.Log("Range State");
        Shoot();
        if (semiBoss.InMeleeRange) {
            semiBoss.ChangeState(new MeleeAttack());
        }else if (semiBoss.Target != null)
        {
            semiBoss.Movement();
        }
        else {
            semiBoss.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter(Collider2D other)
    {
       
    }

    private void Shoot() {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootCD) {
            canShoot = true;
            shootTimer = 0;
        }

        if (canShoot) {
            
            canShoot = false;
            semiBoss.s_anim.SetBool("Range", true);
            // semiBoss.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // cai nay test sau
            //semiBoss.s_anim.SetFloat("spd",0);
        }
    }

}

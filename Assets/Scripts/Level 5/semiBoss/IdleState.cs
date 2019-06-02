using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ISemiBoss
{
    private SemiBoss SemiBoss;
    private float idleTimer;
    private float idleDuration = 5.0f; //time that we allow boss to idle -- after 5s
    public void Enter(SemiBoss semiBoss)
    {
        SemiBoss = semiBoss;
    }

    public void Execute()
    {
       Debug.Log("tao idle");
        Idle();

        if (SemiBoss.Target != null) {
            SemiBoss.ChangeState(new MoveState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            SemiBoss.RotateEnemy();
        }
    }
    private void Idle() {
        SemiBoss.s_anim.SetFloat("spd", 0);
        //idle time increase overtime
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration) {
            SemiBoss.ChangeState(new MoveState());
        }
    }
}

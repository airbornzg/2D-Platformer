using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ISemiBoss
{
    private SemiBoss SemiBoss;
    private Animator s_anim;
    public void Enter(SemiBoss semiBoss)
    {
        SemiBoss = semiBoss;
    }

    public void Execute()
    {
        //Debug.Log("tao idle");
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }
    private void Idle() {
        SemiBoss.s_anim.SetFloat("spd", 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ISemiBoss
{
    private SemiBoss SemiBoss;
    private float idleTimer;
    private float idleDuration; //time that we allow boss to idle -- after 5s
    private player5Controller player5;
   
    public void Enter(SemiBoss semiBoss)
    {
        idleDuration = UnityEngine.Random.Range(1,10); // randomly set idle time
        SemiBoss = semiBoss;
    }

    public void Execute()
    {
       Debug.Log("I'm idle");
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
        /*if (other.tag == "Edge")
        {
            SemiBoss.RotateEnemy();
        }*/
        try
        {
            if (other.tag == "playerBullet")
            {
                SemiBoss.Target = player5.gameObject;
            }
        }
        catch (System.NullReferenceException e)
        {

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

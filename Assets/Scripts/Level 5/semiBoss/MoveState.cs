using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : ISemiBoss
{
    private float moveTimer;
    private float moveDuration = 3.0f;
    private SemiBoss SemiBoss;
    public void Enter(SemiBoss semiBoss)
    {
        SemiBoss = semiBoss;
    }

    public void Execute()
    {
      Debug.Log("tao move");
        Move();
     
        SemiBoss.Movement();

        if (SemiBoss.Target != null && SemiBoss.inShootRange) {
            SemiBoss.ChangeState(new RangeState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Edge") {
            SemiBoss.RotateEnemy();
            
        }
    }
    private void Move()
    {
        
        //idle time increase overtime
        moveTimer += Time.deltaTime;

        if (moveTimer >= moveDuration)
        {
            SemiBoss.ChangeState(new IdleState());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISemiBoss
{
    void Execute(); //call in update 
    void Enter(SemiBoss semiBoss); //call to switch into state
    void Exit(); // exit state
    void OnTriggerEnter(Collider2D other); //double check whether need 2D or not
}

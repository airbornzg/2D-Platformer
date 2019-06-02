﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIgnoreCollision : MonoBehaviour
{
    
    private BoxCollider2D player5Box;
  
    private CircleCollider2D player5Circle;
   
    // Start is called before the first frame update
    private void Awake()
    {
        player5Box = GameObject.Find("player5").GetComponent<BoxCollider2D>();
        player5Circle = GameObject.Find("player5").GetComponent<CircleCollider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player5Box, true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player5Circle, true);
    }

}

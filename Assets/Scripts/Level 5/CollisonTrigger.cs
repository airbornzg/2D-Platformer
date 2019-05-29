using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonTrigger : MonoBehaviour
{
    private BoxCollider2D player5Box;
    private CircleCollider2D player5Circle;
    private CapsuleCollider2D playerCap;
    [SerializeField]
    private BoxCollider2D platformCollider;

    [SerializeField]
    private BoxCollider2D platformTrigger;
    // Start is called before the first frame update
    void Start()
    {
        player5Box = GameObject.Find("player5").GetComponent<BoxCollider2D>();
        player5Circle = GameObject.Find("player5").GetComponent<CircleCollider2D>();
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player5") {
             Physics2D.IgnoreCollision(platformCollider, player5Box, true);
             Physics2D.IgnoreCollision(platformCollider, player5Circle, true);
           
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player5")
        {
             Physics2D.IgnoreCollision(platformCollider, player5Box, false);
             Physics2D.IgnoreCollision(platformCollider, player5Circle, false);
          
        }
    }

}

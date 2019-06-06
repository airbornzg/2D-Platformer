using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    float moveSpd = 7f;
    Rigidbody2D rb;

    GameObject target;
    public int damage = 1;
    LevelManager lvlManager;
    Vector2 moveDirection;
    // Start is called before the first frame update

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("player5");
        if (target != null)
        {
            moveDirection = (target.transform.position - transform.position).normalized * moveSpd;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            lvlManager = FindObjectOfType<LevelManager>();
        }
        if(target == null) { //temporary fix
            Destroy(gameObject);
        }
        
        
        Destroy(gameObject, 3f);
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {

            if (target != null) {
                Debug.Log(collision.name);
                lvlManager.PlayerDamage5(damage);
                Destroy(gameObject);
            }
            
        }
       
    } */
}

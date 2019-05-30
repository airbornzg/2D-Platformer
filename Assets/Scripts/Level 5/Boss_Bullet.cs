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
        if (target.activeSelf) {
            moveDirection = (target.transform.position - transform.position).normalized * moveSpd;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            lvlManager = FindObjectOfType<LevelManager>();
        }
        
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            Debug.Log(collision.name);
            lvlManager.PlayerDamage5(damage);    
            Destroy(gameObject);
        }
       
    }
}

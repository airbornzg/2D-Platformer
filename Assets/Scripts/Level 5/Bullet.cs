using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float flySpd = 20f;
    public Rigidbody2D rb;
    public int damage = 40;
    public GameObject impact;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * flySpd;
    }
     void OnTriggerEnter2D(Collider2D hitBox)
    {
        //Debug.Log(hitBox.name);
      /*  if (hitBox.gameObject.name == "Boss_X") {
            if (hitBox.tag == "B_HitBox") {
                Destroy(gameObject);
            }
        }*/
       Enemy5Script enemy =  hitBox.GetComponent<Enemy5Script>();
        Boss boss = hitBox.GetComponent<Boss>();

        if (boss !=null) {
            Debug.Log(hitBox.name);
            boss.DamageBoss(damage);
        }
        if (enemy != null) {
            enemy.TakeDamage(damage);
           
        }
        Instantiate(impact, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}

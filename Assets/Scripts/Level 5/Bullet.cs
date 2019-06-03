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
        Enemy5Script enemy =  hitBox.GetComponent<Enemy5Script>();
        Boss boss = hitBox.GetComponent<Boss>();
        SemiBoss s_boss = hitBox.GetComponent<SemiBoss>();

        if (boss !=null) {
            Debug.Log(hitBox.name);
            boss.DamageBoss(damage);
        }

        if (enemy != null) {
            enemy.TakeDamage(damage);
           
        }
        if (s_boss != null) {
            s_boss.TakeDamage(damage);
        }
  

        GameObject clone = Instantiate(impact, transform.position, transform.rotation); // store instantiate impact to clone
        Destroy(gameObject);
        Destroy(clone,1.0f); // destroy clone after 1s
      
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}

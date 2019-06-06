using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBulletController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public GameObject enemy;
    private float enemyLifes = 3;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * 20;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}

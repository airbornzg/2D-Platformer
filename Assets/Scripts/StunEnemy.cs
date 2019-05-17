 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEnemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] float bounceForce;

    private Rigidbody2D playerRigidbody2D;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);

            Instantiate(deathVFX, other.transform.position, other.transform.rotation);

            playerRigidbody2D.velocity = new Vector3(playerRigidbody2D.velocity.x, bounceForce, 0f);
        }
    }
}

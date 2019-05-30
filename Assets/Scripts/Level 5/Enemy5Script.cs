using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Script : MonoBehaviour
{
    public int enemyHealth = 100; //initial setup for enemy health
    // Start is called before the first frame update
    public GameObject deathEffect;
    private bool FacingRigt = false;
   

    public void TakeDamage(int damage) {

        enemyHealth -= damage;

        if (enemyHealth <= 0) {
            Death();
        // cai o duoi de test later bc when respawn enemy hp not reset
            // enemyHealth = 100;
        }
    }

     void Death() {
       GameObject clone = Instantiate(deathEffect,transform.position, Quaternion.identity);
        //Destroy(gameObject);
        gameObject.SetActive(false); // if we destroy object, it lost everything while the respawn method just based on the logic of object 
        Destroy(clone, 1.0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Edge") {
            Rotation();
        }
    }

    void Rotation() {
        FacingRigt = !FacingRigt;
        transform.Rotate(0f, 180f, 0f);
    }
}

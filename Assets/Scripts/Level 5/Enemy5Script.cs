using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Script : MonoBehaviour
{
    public int enemyHealth = 100; //initial setup for enemy health
    // Start is called before the first frame update
    public GameObject deathEffect;
    
   

    public void TakeDamage(int damage) {

        enemyHealth -= damage;

        if (enemyHealth <= 0) {
            Death();
        // cai o duoi de test later bc when respawn enemy hp not reset
            // enemyHealth = 100;
        }
    }

     void Death() {
        Instantiate(deathEffect,transform.position, Quaternion.identity);
        //Destroy(gameObject);
        gameObject.SetActive(false); // if we destroy object, it lost everything while the respawn method just based on the logic of object 
      
    }
}

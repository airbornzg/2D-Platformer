﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Boss : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject target;
    Vector3 targetDirection; // this one use for identify move direcxtion can remove later
    public float moveSpd = 2f; // same
    bool isAttack = false;
    
    public GameObject bullet;
    public Transform fireBox;
    public Transform fireBox2;
    public int bossHealth = 400;
    public GameObject bossDead;
  
    float fireRate;
    float nextFire;
    float Distance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("player5");
        fireRate = 4f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        targetDirection = (target.transform.position - transform.position);
        Distance = targetDirection.magnitude;
        targetDirection /= Distance;

        if (Distance < 12)
        {
            isAttack = true;
          
        }
        else {
            isAttack = false;
        }
        Attacking();
        
    }
  
    void Attacking() {
        if (isAttack)
        {
            if (Time.time > nextFire)
            {
                Instantiate(bullet, fireBox.position, fireBox.rotation);
                Instantiate(bullet, fireBox2.position, fireBox2.rotation);
                nextFire = Time.time + fireRate;
            }
        }
        else {
          //  Debug.Log("Not in range");
        }
      
    }
    public void DamageBoss(int damage) {
        bossHealth -= damage;

        if (bossHealth <=0) {
            Dead();
            SceneManager.LoadScene("WinScene");
        }

    }

    void Dead() {
        Instantiate(bossDead, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
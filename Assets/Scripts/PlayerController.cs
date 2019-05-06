﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 10f;

    public Vector3 respawnPosition;

    private Rigidbody2D myRigidbody2d;
    private Animator myAnim;

    private bool isGrounded = false;

    public LevelManager theLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2d = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        theLevelManager = FindObjectOfType<LevelManager>();

        //Initial respawn position without any other checkpoint
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerJump();
        PlayerAnimationControl();
    }

    private void PlayerAnimationControl()
    {
        myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody2d.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);
    }

    private void PlayerMovement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            myRigidbody2d.velocity = new Vector3(moveSpeed, myRigidbody2d.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            myRigidbody2d.velocity = new Vector3(-moveSpeed, myRigidbody2d.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            myRigidbody2d.velocity = new Vector3(0f, myRigidbody2d.velocity.y, 0f);
        }
    }

    private void PlayerJump()
    {
        isGrounded = System.Math.Abs(myRigidbody2d.velocity.y) <= Mathf.Epsilon;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            myRigidbody2d.velocity = new Vector3(myRigidbody2d.velocity.x, jumpHeight, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "KillPlane")
        {
            //transform.position = respawnPosition;
            theLevelManager.Respawn();
        }

        if (other.tag == "Checkpoint")
        {
            respawnPosition = other.transform.position;
        }
    }
}
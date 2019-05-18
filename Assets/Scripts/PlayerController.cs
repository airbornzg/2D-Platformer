using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] float jumpHeight;

    [SerializeField] GameObject stunBox;

    //Pushed back force when damaged
    [SerializeField] float pushedBackForce;
    [SerializeField] float pushedbackTime;
    [SerializeField] float pushedBackTimeCounter;

    //Invicible after damage
    [SerializeField] float invincibleTime;
    private float invincibleCounter;

    //Level Management
    public LevelManager theLevelManager;

    //Respawn Position
    public Vector3 respawnPosition;

    //Player Animation 
    public Rigidbody2D myRigidbody2d;
    private Animator myAnim;

    public bool canMove;

    private bool isGrounded = false;

    private int killPlayerDamage;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2d = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        theLevelManager = FindObjectOfType<LevelManager>();

        //Define the instant kill damage
        killPlayerDamage = theLevelManager.maxHealth;

        //Initial respawn position without any other checkpoint
        respawnPosition = transform.position;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (pushedBackTimeCounter <= 0 && canMove)
        {
            PlayerMovement();
            PlayerJump();
            
            if (invincibleCounter > 0)
            {
                invincibleCounter -= Time.deltaTime;
            }

            if(invincibleCounter <= 0)
            {
                theLevelManager.isInvincible = false;
            }
        }

        if (pushedBackTimeCounter > 0)
        {
            pushedBackTimeCounter -= Time.deltaTime;
            if (transform.localScale.x > 0)
            {
                myRigidbody2d.velocity = new Vector3(-pushedBackForce, pushedBackForce * 0.5f, 0f);
            }
            else
            {
                myRigidbody2d.velocity = new Vector3(pushedBackForce, pushedBackForce * 0.5f, 0f);
            }
        }

        PlayerAnimationControl();

        if (myRigidbody2d.velocity.y < 0)
        {
            stunBox.SetActive(true);
        }
        else
        {
            stunBox.SetActive(false);
        }
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
        if(other.tag == "Death Block")
        {
            theLevelManager.PlayerDamage(killPlayerDamage);
            theLevelManager.Respawn();
        }

        if (other.tag == "Checkpoint")
        {
            respawnPosition = other.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    public void PushedBack()
    {
        pushedBackTimeCounter = pushedbackTime;
        invincibleCounter = invincibleTime;
        theLevelManager.isInvincible = true;
    }
}

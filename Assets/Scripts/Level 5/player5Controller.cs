using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player5Controller : MonoBehaviour
{
   // public MovementController m_controller;
    public player5Movement playerMovement;
    public Animator player5_Anim;
    float horizonMove = 0f;
    public float moveSpd = 0.5f;
    bool onJump = false;
    bool onCrouch = false;
    bool shotClicked = false;
    public LevelManager lvlManager;
    private int killPlayerDamage;
    public Vector3 respawnPosition;

    
    // Start is called before the first frame update
    void Start()
    {
        lvlManager = FindObjectOfType<LevelManager>();

        //Define the instant kill damage
        killPlayerDamage = lvlManager.maxHealth;

        //Initial respawn position without any other checkpoint
        respawnPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        horizonMove = Input.GetAxisRaw("Horizontal") * moveSpd;

        player5_Anim.SetFloat("speed", Mathf.Abs(horizonMove)); //mathf.abs -> return absolute value
        if (Input.GetButtonDown("Jump")) {
            onJump = true; // set the value to true for jump
            player5_Anim.SetBool("onGround", false);
        }

        if (Input.GetButtonDown("Crouch")) {
            onCrouch = true;
        }else if (Input.GetButtonUp("Crouch")) {
            onCrouch = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            shotClicked = true;
            player5_Anim.SetBool("Shot",shotClicked);
           

        }
        else {
            shotClicked = false;
            player5_Anim.SetBool("Shot", shotClicked);
          
        }
    }

    public void OnGround() {
        player5_Anim.SetBool("onGround", true);
    }

    public void onCrouching  (bool isCrouching) {
           
        player5_Anim.SetBool("isCrouch", isCrouching);
    }
   
    void FixedUpdate()
    {
        // m_controller.Move(horizonMove * Time.fixedDeltaTime ,onCrouch,onJump);
        playerMovement.Move(horizonMove * Time.fixedDeltaTime, onCrouch, onJump);
        onJump = false; //reset the value of onground back to false
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Death Block")
        {
           lvlManager.PlayerDamage5(killPlayerDamage);
           lvlManager.Respawn5();
        }

        if (collision.tag == "Checkpoint")
        {
            respawnPosition = collision.transform.position;
        }
    }
}

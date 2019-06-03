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
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource gunAudio;
    public GameObject WallLeft; // cai nay chut xu ly tip sau khi xong boss
    public GameObject WallRight;
    private bool isDeath;
    [SerializeField]
    private GameObject semiboss;

    [SerializeField]
    private int enemyDamage;

    [SerializeField]
    private int bossDamage;

    public SpriteRenderer player5_render;

    private bool immortal = false;

    [SerializeField]
    private float immortalTime;
    
    //cai animation hurt de xem add sau 

    // Start is called before the first frame update
    void Start()
    {
        lvlManager = FindObjectOfType<LevelManager>();
        

        player5_render = GetComponent<SpriteRenderer>();
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
            jumpAudio.Play();
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
            gunAudio.Play();
           

        }
        else {
            shotClicked = false;
            player5_Anim.SetBool("Shot", shotClicked);
          
        }
        if (transform.position.x >= 125 && semiboss.activeSelf)
        {
            WallLeft.SetActive(true);
            WallRight.SetActive(true);
        }
        if (!semiboss.activeSelf) {
            WallLeft.SetActive(false);
            WallRight.SetActive(false);
        }
        if (lvlManager.maxHealth <= 0)
        {
            isDeath = true;
        }
        else {
            isDeath = false;
        }
    }

    public void OnGround() {
        if (GetComponent<Rigidbody2D>().velocity.y <= 0) {
            player5_Anim.SetBool("onGround", true);
        }
        
    }

    public void onCrouching  (bool isCrouching) {
           
        player5_Anim.SetBool("isCrouch", isCrouching);
    }
    void notImmortal() {
        immortal = false;
    }

    private IEnumerator IndicateImmortal() {
        
            while (immortal)
            {
                player5_render.enabled = false;

                yield return new WaitForSeconds(0.1f);

                player5_render.enabled = true;

                yield return new WaitForSeconds(0.1f);
            }
       
        
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
            WallLeft.SetActive(false);
            WallRight.SetActive(false);
        }
        if (collision.tag == "S_Melee") {
            if (!immortal) {
                if (!isDeath)
                {
                    lvlManager.PlayerDamage5(bossDamage);
                    immortal = true;
                    StartCoroutine(IndicateImmortal());
                    Invoke("notImmortal", immortalTime);
                }
            }
        }
        if (collision.tag == "Enemy") {
            if (!immortal)
            {
                if (!isDeath)
                {
                    lvlManager.PlayerDamage5(enemyDamage);
                    immortal = true;
                    StartCoroutine(IndicateImmortal());
                    Invoke("notImmortal", immortalTime);
                }
                else {
                    lvlManager.Respawn5();
                }
            }
        }
        if (collision.tag == "Boss_bullet") {
            if (gameObject.activeSelf) {
                if (!immortal)
                {
                    if (!isDeath)
                    {
                        lvlManager.PlayerDamage5(bossDamage);
                        Destroy(collision.gameObject);
                        immortal = true;
                        StartCoroutine(IndicateImmortal());
                        Invoke("notImmortal", immortalTime);
                    }
                }
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiBoss : MonoBehaviour
{
    private ISemiBoss currentState;
    public Animator s_anim;
    private bool facingRight = false;
    public float moveSpd;
    public Transform fireBox;
    public GameObject bullet;
    public bool canAttack;
    
    public int bossHealth;
    private bool isDeath;
    public GameObject WallLeft; 
    public GameObject WallRight;
    [SerializeField]
    private EdgeCollider2D meleePoint;

    public bool TakingDamage { get; set; }
    public GameObject Target { get; set; }

    [SerializeField]
    private float meleeRange;
    public bool InMeleeRange
    {
        get
        {
            if (Target != null) {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }

    [SerializeField]
    private float shootRange;

    public bool InShootRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= shootRange;
            }
            return false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new IdleState()); // set the first state is idle
       
    }

    // Update is called once per frame
    void Update()
    {

        if (bossHealth <= 0)
        {
            isDeath = true;
        }
        else
        {
            isDeath = false;
        }

        if (!isDeath)
        {

            if (!TakingDamage)
            {
                currentState.Execute();
            }

            LookAtTarget();
        }
       
        
    }
    private void LookAtTarget() {

        if (Target != null) {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0 && facingRight || xDir > 0 && !facingRight) {
                RotateEnemy();
            }
        }
    }

    public void ChangeState(ISemiBoss newState) {
        if (currentState != null) {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);

    }
    public void Movement() {
        if (!canAttack) {
            s_anim.SetFloat("spd", 1);
            // tell unity to get direction of boss and add move to that direction with given spd
            transform.Translate(GetDirection() * (moveSpd * Time.deltaTime));
        }

    }
    public Vector2 GetDirection() {
        return facingRight ? Vector2.right : Vector2.left;
    }
  
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
       currentState.OnTriggerEnter(collision); // this one work properly the
        /*
         * the issue that why it not work in the 1st time is because the collider
         * double collider with normal and trigger
         * some how cause an issue with rotating
         */
        //Debug.Log("come here");
    }
      public void RotateEnemy()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x* -1,1,1);
    }
    public void Attacking()
    {
        if (facingRight)
        {
            Instantiate(bullet, fireBox.position, fireBox.rotation);

        }
        else
        {
            Instantiate(bullet, fireBox.position, fireBox.rotation);
        }
    }
    public void MeleeAtk() {
        // Debug.Log("attacked");
        meleePoint.enabled = !meleePoint.enabled;
    }

    public void TakeDamage(int damage) {
        bossHealth -= damage;

        if (!isDeath)
        {
            s_anim.SetTrigger("Damage");
        }
        else
        {
            s_anim.SetTrigger("Death");
            gameObject.SetActive(false);
           
        }
    }
}

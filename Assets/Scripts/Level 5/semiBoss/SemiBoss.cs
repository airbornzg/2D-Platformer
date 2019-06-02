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

    [SerializeField]
    private float MeleeRange;
    [SerializeField]
    private float ShootRange;
    public bool inMeleeRange {
        get {
            if (Target != null) {
                return Vector2.Distance(transform.position, Target.transform.position) <= MeleeRange;
            }
            return false;
        }
    }

    public bool inShootRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= ShootRange;
            }
            return false;
        }
    }

    public GameObject Target { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new IdleState()); // set the first state is idle
       // Target = GameObject.Find("player5");
    }

    // Update is called once per frame
    void Update()
    {
       
        currentState.Execute();

        LookAtTarget();
        
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
        s_anim.SetFloat("spd",1);
        // tell unity to get direction of boss and add move to that direction with given spd
        transform.Translate(GetDirection() * (moveSpd * Time.deltaTime));

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
        Debug.Log("attacked");
    }
}

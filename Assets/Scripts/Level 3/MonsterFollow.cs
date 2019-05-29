
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFollow : MonoBehaviour
{

    private Animator anim;
    private GameObject player;
    private LevelManager theLevelManager;
    private GameObject groundTiles;

    private Vector2 targetPosition;
    public float speed = 5.0f;

    public Transform firepoint;
    public GameObject firePrefab;
    float fireRate;
    float nextFire;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        theLevelManager = FindObjectOfType<LevelManager>();
        groundTiles = GameObject.FindWithTag("Ground");

        transform.position = new Vector2(player.transform.position.x - 12, player.transform.position.y);
        fireRate = 1.16f;
        nextFire = Time.time;
        //moveForward();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Vector2.Distance(player.transform.position, transform.position) < 5)
        {
            anim.SetBool("attack", true);
            CheckIfTimeToFire();
        }
        else
        {
            anim.SetBool("attack", false);
            targetPosition = new Vector2(player.transform.position.x, player.transform.position.y + 0.5f);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 3 * Time.deltaTime);
        }
    }

    void CheckIfTimeToFire ()
    {
        if(Time.time > nextFire)
        {
            Instantiate(firePrefab, firepoint.position, firepoint.rotation);
            nextFire = Time.time + fireRate;
        }


    }

    void OnCollisionStay(Collision collisionInfo)
    {
        print("true");
    }

    //private void moveForward()
    //{
    //    int i = 0;
    //    while (i < groundTiles.transform.childCount)
    //    {
    //        GameObject tile = groundTiles.transform.GetChild(i).gameObject;

    //        while(transform.GetComponent<Rigidbody2D>())
    //    }
    //}

    //void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    // only check lateral collisions
    //    if (Mathf.Abs(hit.normal.y) < 0.5)
    //    {
    //        jump = true; // jump if collided laterally
    //    }
    //}

    //private bool CheckMonsterIsInCameraView()
    //{
    //    Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);

    //    if (screenPoint.x < 0)
    //    {
    //        transform.position = new Vector2(Camera.main.transform.position.x - (Camera.main.fieldOfView * 0.5f), transform.position.y);
    //        return false;
    //    }

    //    return true;
    //}
}

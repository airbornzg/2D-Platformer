using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl_Level2 : MonoBehaviour
{

    private Animator anim;
    private GameObject player;
    private LevelManager theLevelManager;
    private GameObject groundTiles;

    private Vector2 targetPosition;
    public float speed = 5.0f;
  
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        theLevelManager = FindObjectOfType<LevelManager>();
        groundTiles = GameObject.FindWithTag("Ground");

        transform.position = new Vector2(player.transform.position.x - 10, player.transform.position.y);
        //moveForward();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Vector2.Distance(player.transform.position, transform.position) < 1)
        {
            anim.SetBool("attack", true);
            StartCoroutine(DelayUpdate());
        }
        else
        {
            targetPosition = new Vector2(player.transform.position.x, player.transform.position.y + 0.5f);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 3 * Time.deltaTime);
        }
    }
   
    private IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(1.8f);
        anim.SetBool("attack", false);
        theLevelManager.PlayerDamage(theLevelManager.maxHealth);
        theLevelManager.Respawn();
    }

    private IEnumerator DelayMonsterMovement(float delay)
    {
        yield return new WaitForSeconds(1.8f);
        LateUpdate();
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        print("true");
    }

    //    void Start()
    //    {
    //        anim = GetComponent<Animator>();
    //        player = GameObject.FindWithTag("Player");
    //        theLevelManager = FindObjectOfType<LevelManager>();

    //        storedPositions = new List<Vector3>(); //create a blank list
    //    }

    //    void LateUpdate()
    //    {
    //        if (storedPositions.Count == 0)
    //        {
    //            Debug.Log("blank list");
    //            storedPositions.Add(player.transform.position); //store the players currect position
    //            return;
    //        }
    //        else if (storedPositions[storedPositions.Count - 1] != player.transform.position)
    //        {
    //            //Debug.Log("Add to list");
    //            storedPositions.Add(player.transform.position); //store the position every frame
    //        }
    //        //else if (storedPositions[storedPositions.Count - 1] == player.transform.position)
    //        //{
    //        //    transform.position = storedPositions[0]; //move
    //        //    storedPositions.RemoveAt(0);
    //        //}

    //        if (storedPositions.Count > followDistance)
    //        {
    //            transform.position = storedPositions[0]; //move
    //            storedPositions.RemoveAt(0); //delete the position that player just move to
    //        }
    //    }

    //    private void OnCollisionEnter2D(Collision2D collision)
    //    {
    //        if (collision.gameObject.tag == "Player")
    //        {
    //            anim.SetBool("attack", true);
    //            StartCoroutine(DelayUpdate());
    //        } 
    //    }

    //    private IEnumerator DelayUpdate()
    //    {
    //        yield return new WaitForSeconds(1.8f);
    //        anim.SetBool("attack", false);
    //        theLevelManager.PlayerDamage(theLevelManager.maxHealth);
    //        theLevelManager.Respawn();
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

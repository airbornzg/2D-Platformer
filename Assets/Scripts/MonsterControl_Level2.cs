using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl_Level2 : MonoBehaviour
{

    private Animator anim;
    private GameObject player;
    private LevelManager theLevelManager;

    private List<Vector3> storedPositions;
    [SerializeField] private int followDistance = 100;
    public float monsterOffsetY;

    void Start() 
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        theLevelManager = FindObjectOfType<LevelManager>();

        SetupStoredPositions();
    }

    private void LateUpdate()
    {
        storedPositions.Add(new Vector2(player.transform.position.x, player.transform.position.y + monsterOffsetY));

        print(storedPositions.Count);
        if (storedPositions.Count > followDistance)
        {
            transform.position = storedPositions[0]; //move monster 
            storedPositions.RemoveAt(0); //delete the position that player just move to
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("attack", true);
            StartCoroutine(DelayUpdate());
        } 
    }

    private IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(1.8f);
        anim.SetBool("attack", false);
        theLevelManager.PlayerDamage(theLevelManager.maxHealth);
        theLevelManager.Respawn();
    }

    public void SetupStoredPositions()
    {
        storedPositions = new List<Vector3>(); //create a blank list
        for (int i = 0; i < 100; i++)
        {
            storedPositions.Add(new Vector2(player.transform.position.x - 8, player.transform.position.y + monsterOffsetY));
        }
    }
}
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

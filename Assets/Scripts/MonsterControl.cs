using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rigidbody;
    private Vector2 targetPosition;
    private float distance = 5.0f;
    public float speed = 0.2f;
    public LevelManager theLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        theLevelManager = FindObjectOfType<LevelManager>();


    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(player.transform.position, transform.position) < 0.1)
        {
            theLevelManager.Respawn();
            transform.position = new Vector2(player.transform.position.x - 10.0f, player.transform.position.y);
        }
        //transform.LookAt(player.transform.position);

        targetPosition = new Vector2(transform.position.x + speed, player.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 5.0f * Time.deltaTime);
    }
}

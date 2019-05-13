using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rigidbody;
    private Vector2 targetPosition;
    private float distance = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector2(player.transform.position.x - distance, player.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 5.0f * Time.deltaTime);
    }
}

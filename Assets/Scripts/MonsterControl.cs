using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{

    private Animator anim;
    private GameObject player;

    private Vector2 targetPosition;
    private float distance = 5.0f;
    public float speed = 5.0f;
    public LevelManager theLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        theLevelManager = FindObjectOfType<LevelManager>();

        transform.position = new Vector2(player.transform.position.x - 10, player.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(player.transform.position, transform.position) < 0.1)
        {
            theLevelManager.PlayerDamage(theLevelManager.maxHealth);
            theLevelManager.Respawn();
        }

        targetPosition = new Vector2(transform.position.x + speed, player.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 4 * Time.deltaTime);
    }
}

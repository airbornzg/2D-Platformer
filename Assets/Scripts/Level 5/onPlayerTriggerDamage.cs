using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onPlayerTriggerDamage : MonoBehaviour
{
    [SerializeField]
    private LevelManager theLevelManager;

    [SerializeField]
    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            theLevelManager.PlayerDamage5(damage);
        }
    }
}

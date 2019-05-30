using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    [SerializeField] LevelManager theLevelManager;

    [SerializeField] int damageToGive;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theLevelManager.PlayerDamage(damageToGive);
            StartCoroutine(Flash(0.1f));
        }
    }

    IEnumerator Flash(float x)
    {
        //player.GetComponent<CapsuleCollider2D>().enabled = false;
        for (int i = 0; i < 5; i++)
        {
            player.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(x);
            player.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(x);
        }
        player.GetComponent<SpriteRenderer>().enabled = true;
    }
}
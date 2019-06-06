using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterControl_Level2 : MonoBehaviour
{

    private Animator anim;
    private GameObject player;
    private LevelManager theLevelManager;
    private Text monsterText;

    private List<Vector3> storedPositions;
    [SerializeField] private int followDistance = 100;
    public float monsterOffsetY;

    void Start() 
    {
        monsterText = GameObject.Find("MonsterText").GetComponent<Text>();

        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        theLevelManager = FindObjectOfType<LevelManager>();

        SetupStoredPositions(80);
    }

    private void LateUpdate()
    {

        if (CheckIfPlayerIsInCameraView())
        {
            StartCoroutine(RemoveAfterSeconds(5, monsterText));
            storedPositions.Add(new Vector2(player.transform.position.x, player.transform.position.y + monsterOffsetY));

            print(storedPositions.Count);
            if (storedPositions.Count > followDistance)
            {
                transform.position = storedPositions[0]; //move monster 
                storedPositions.RemoveAt(0); //delete the position that player just move to
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
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
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void SetupStoredPositions(int delay)
    {
        storedPositions = new List<Vector3>(); //create a blank list
        for (int i = 0; i <= delay; i++)
        {
            storedPositions.Add(new Vector2(player.transform.position.x - 5, player.transform.position.y + monsterOffsetY));
        }
    }

    private bool CheckIfPlayerIsInCameraView()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(player.transform.position);

        if (screenPoint.x < 0)
        {
            return false;
        }

        return true;
    }

    private IEnumerator RemoveAfterSeconds(int seconds, Text obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.gameObject.SetActive(false);
    }
}

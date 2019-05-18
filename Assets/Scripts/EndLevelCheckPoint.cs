using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelCheckPoint : MonoBehaviour
{

    [SerializeField] string levelLoader;

    private PlayerController thePlayer;
    private FollowCamera theFollowCamera;
    private LevelManager theLevelManager;
    private bool canMovePlayer;

    [SerializeField] float EndLevelMovingTime;
    [SerializeField] float EndLevelLoadingTime;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theLevelManager = FindObjectOfType<LevelManager>();
        theFollowCamera = FindObjectOfType<FollowCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMovePlayer)
        {
            thePlayer.myRigidbody2d.velocity = new Vector3(thePlayer.moveSpeed, thePlayer.myRigidbody2d.velocity.y, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("EndLevelCheckPointCoroutine");
        }
    }

    public IEnumerator EndLevelCheckPointCoroutine()
    {
        thePlayer.canMove = false;
        theFollowCamera.followPlayer = false;
        theLevelManager.isInvincible = true;

        thePlayer.myRigidbody2d.velocity = Vector3.zero;

        PlayerPrefs.SetInt("ScoreCount", theLevelManager.scoreCount);
        PlayerPrefs.SetInt("LifeCount", theLevelManager.currentLifeNumber);

        yield return new WaitForSeconds(EndLevelMovingTime);

        canMovePlayer = true;

        yield return new WaitForSeconds(EndLevelLoadingTime);
        SceneManager.LoadScene(levelLoader);

    }
}

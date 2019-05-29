using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //Respawn
    [SerializeField] float waitToRespawn;
    [SerializeField] GameObject deathVFX;

    private bool respawning = false;

    [SerializeField] GameResetOnRespawn[] gameObjectToReset; 

    //Score
    public int scoreCount;
    [SerializeField] Text scoreText;

    [SerializeField] int scoreExtraLifeCount;
    [SerializeField] int ScoreToRedeem;

    //Health Meter
    [SerializeField] Image heart1;
    [SerializeField] Image heart2;
    [SerializeField] Image heart3;

    [SerializeField] Sprite heartFull;
    [SerializeField] Sprite heartHalf;
    [SerializeField] Sprite heartEmpty;

    [SerializeField] int healthCount;
    public int maxHealth;
    
    //Invincibility
    public bool isInvincible;


    //Life
    public int currentLifeNumber;
    [SerializeField] int startingLifeNumber;
    [SerializeField] Text lifeText;

    //Game Over
    [SerializeField] GameObject gameOverWindow;

    //Reference
    private PlayerController thePlayer;
    private MonsterControl theMonster;

    //Monster
    private GameObject monster;
    

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theMonster = FindObjectOfType<MonsterControl>();
        gameObjectToReset = FindObjectsOfType<GameResetOnRespawn>();

        healthCount = maxHealth;

        PreviousLevelParameterCheck();

        scoreText.text = "Score: " + scoreCount.ToString();
        lifeText.text = "Life left: " + currentLifeNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }

        if (scoreExtraLifeCount >= ScoreToRedeem)
        {
            currentLifeNumber += 1;
            lifeText.text = "Life left: " + currentLifeNumber;
            scoreExtraLifeCount -= ScoreToRedeem;
        }
    }

    private void PreviousLevelParameterCheck()
    {
        if (PlayerPrefs.HasKey("ScoreCount"))
        {
            scoreCount = PlayerPrefs.GetInt("ScoreCount");
        }

        if (PlayerPrefs.HasKey("LifeCount"))
        {
            currentLifeNumber = PlayerPrefs.GetInt("LifeCount");
        }
        else
        {
            currentLifeNumber = startingLifeNumber;
        }
    }

    public void Respawn()
    {
        if (!respawning)
        {
            respawning = true;
            currentLifeNumber -= 1;
            lifeText.text = "Life left: " + currentLifeNumber;

            if (currentLifeNumber > 0)
            {
                StartCoroutine("RespawnCoroutine");
            }
            else
            {
                thePlayer.gameObject.SetActive(false);
                gameOverWindow.SetActive(true);
            }
        }
    }

    //Delay the respawning process for few seconds
    public IEnumerator RespawnCoroutine()
    {
        thePlayer.gameObject.SetActive(false);
        if (GameObject.FindGameObjectsWithTag("Monster").Length < 0)
        {
            theMonster.gameObject.SetActive(false);
        }
        StartDeathVFX();

        yield return new WaitForSeconds(waitToRespawn);

        //Update the socre when player die
        ResetScore();

        UpdateRespawnHealth();
        UpdateRespawnPos();

        for (int i = 0; i < gameObjectToReset.Length; i++)
        {
            gameObjectToReset[i].gameObject.SetActive(true);
            gameObjectToReset[i].ResetObject();
        }
    }

    private void ResetScore()
    {
        scoreCount = 0;
        scoreText.text = "Score: " + scoreCount.ToString();
        scoreExtraLifeCount = 0;
    }

    private void StartDeathVFX()
    {
        Instantiate(deathVFX, thePlayer.transform.position, thePlayer.transform.rotation);
    }

    private void UpdateRespawnHealth()
    {
        healthCount = maxHealth;
        respawning = false;
        UpdateHealthMeter();
    }

    private void UpdateRespawnPos()
    {
        thePlayer.transform.position = thePlayer.respawnPosition;

        thePlayer.transform.localScale = new Vector3(1f, 1f, 1f);

        thePlayer.gameObject.SetActive(true);

        if (GameObject.FindGameObjectsWithTag("Monster").Length < 0)
        {
            theMonster.transform.position = new Vector2(thePlayer.transform.position.x - 10, thePlayer.transform.position.y);
            theMonster.gameObject.SetActive(true);
        }
    }

    public void AddCoins(int scoreToAdd)
    {
        scoreCount += scoreToAdd;
        scoreExtraLifeCount += scoreToAdd;

        scoreText.text = "Score: " + scoreCount.ToString();
    }

    public void PlayerDamage(int damageToTake)
    {
        if (!isInvincible)
        {
            healthCount -= damageToTake;
            UpdateHealthMeter();
            if (healthCount > 0)
            {
                thePlayer.PushedBack();
            }
        }
    }

    public void UpdateHealthMeter()
    {
        switch (healthCount)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;
            case 5:
                heart1.sprite = heartHalf;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;
            case 4:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;
            case 3:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartHalf;
                heart3.sprite = heartFull;
                return;
            case 2:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartFull;
                return;
            case 1:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartHalf;
                return;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
        }
    }

    public void AddExtraLife (int addLife)
    {
        currentLifeNumber += addLife;
        lifeText.text = "Life left: " + currentLifeNumber;
    }

    public void AddExtraHealth(int addHealth)
    {
        healthCount += addHealth;

        if (healthCount > maxHealth)
        {
            healthCount = maxHealth;
        }

        UpdateHealthMeter();
    }
}

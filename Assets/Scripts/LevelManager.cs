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
    public int startingLifeNumber;
    [SerializeField] Text lifeText;

    //Game Over
    [SerializeField] GameObject gameOverWindow;

    //Reference
    private PlayerController thePlayer;
    private player5Controller thePlayer5;

    //Monster
    private GameObject monster;
    private MonsterControl_Level2 theMonster;

    //Audio
    [SerializeField] AudioSource collectibleAudio;
    

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        thePlayer5 = FindObjectOfType<player5Controller>();
        gameObjectToReset = FindObjectsOfType<GameResetOnRespawn>();
        theMonster = FindObjectOfType<MonsterControl_Level2>();

        if (GameObject.FindGameObjectsWithTag("Monster").Length > 0)
        {
            monster = GameObject.FindWithTag("Monster");
        }

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
            if (thePlayer != null) {
                Respawn();
               
            }
            
            if (thePlayer5 != null) {
                Respawn5();
                thePlayer5.player5_render.enabled = true;
            }
            
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
                if (monster != null)
                {
                    monster.SetActive(false);
                }
                gameOverWindow.SetActive(true);
            }
        }
    }

    //repeated function for scene 5 
    // as the player is fixed
    public void Respawn5()
    {
        if (!respawning)
        {
            respawning = true;
            currentLifeNumber -= 1;
            lifeText.text = "Life left: " + currentLifeNumber;

            if (currentLifeNumber > 0)
            {
                StartCoroutine("RespawnCoroutine5");
            }
            else
            {
                thePlayer5.gameObject.SetActive(false);
                gameOverWindow.SetActive(true);
            }
        }
    }

    //Delay the respawning process for few seconds
    public IEnumerator RespawnCoroutine()
    {
        thePlayer.gameObject.SetActive(false);
        if (monster != null)
        {
            monster.SetActive(false);
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

    //re-use the courtines with different player
    //in this case player 5 was used
    public IEnumerator RespawnCoroutine5()
    {
        thePlayer5.gameObject.SetActive(false);
        StartDeathVFX5();

        yield return new WaitForSeconds(waitToRespawn);

        //Update the socre when player die
        ResetScore();

        UpdateRespawnHealth();
        UpdateRespawnPos5();

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

    //change the position of instantiate death to player5
    private void StartDeathVFX5()
    {
        Instantiate(deathVFX, thePlayer5.transform.position, thePlayer5.transform.rotation);
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

        if (monster != null)
        {
            theMonster.SetupStoredPositions(80);
            monster.gameObject.SetActive(true);
        }
    }

    //re-use and change the player to player 5
    private void UpdateRespawnPos5()
    {
        thePlayer5.transform.position = thePlayer5.respawnPosition;


        thePlayer5.transform.localScale = new Vector3(1f, 1f, 1f);

        thePlayer5.gameObject.SetActive(true);
        

    }

    public void AddCoins(int scoreToAdd)
    {
        scoreCount += scoreToAdd;
        scoreExtraLifeCount += scoreToAdd;

        scoreText.text = "Score: " + scoreCount.ToString();

        collectibleAudio.Play();
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
                thePlayer.damageAudio.Play();
            }
        }
    }

    //re-use and change the player to player 5
    public void PlayerDamage5(int damageToTake)
    {
        if (!isInvincible)
        {
            healthCount -= damageToTake;
            UpdateHealthMeter();
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
        collectibleAudio.Play();
        currentLifeNumber += addLife;
        lifeText.text = "Life left: " + currentLifeNumber;
    }

    public void AddExtraHealth(int addHealth)
    {
        collectibleAudio.Play();
        healthCount += addHealth;

        if (healthCount > maxHealth)
        {
            healthCount = maxHealth;
        }

        UpdateHealthMeter();
    }
}

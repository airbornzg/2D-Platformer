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

    //Coins
    [SerializeField] int scoreCount;
    [SerializeField] Text scoreText;

    //Health Meter
    [SerializeField] Image heart1;
    [SerializeField] Image heart2;
    [SerializeField] Image heart3;

    [SerializeField] Sprite heartFull;
    [SerializeField] Sprite heartHalf;
    [SerializeField] Sprite heartEmpty;

    [SerializeField] int healthCount;
    public int maxHealth;
    
    //Reference
    private PlayerController thePlayer;
    private MonsterControl theMonster;
    

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theMonster = FindObjectOfType<MonsterControl>();

        scoreText.text = "Score: " + scoreCount.ToString();

        healthCount = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }
    }

    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    //Delay the respawning process for few seconds
    public IEnumerator RespawnCoroutine()
    {
        thePlayer.gameObject.SetActive(false);
        theMonster.gameObject.SetActive(false);
        StartDeathVFX();

        yield return new WaitForSeconds(waitToRespawn);

        UpdateRespawnHealth();
        UpdateRespawnPos();
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
        thePlayer.gameObject.SetActive(true);

        theMonster.transform.position = new Vector2(thePlayer.transform.position.x - 10, thePlayer.transform.position.y);
        theMonster.gameObject.SetActive(true);
    }

    public void AddCoins(int coinsToAdd)
    {
        scoreCount += coinsToAdd;
        scoreText.text = "Score: " + scoreCount.ToString();
    }

    public void PlayerDamage(int damageToTake)
    {
        healthCount -= damageToTake;
        UpdateHealthMeter();
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
}

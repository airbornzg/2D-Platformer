using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] string tutorialLevel;
    [SerializeField] string continueLevel;
    [SerializeField] string levelSelection;

    [SerializeField] int startingLifeNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(tutorialLevel);

        PlayerPrefs.SetInt("ScoreCount", 0);
        PlayerPrefs.SetInt("LifeCount", startingLifeNumber);
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene(levelSelection);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

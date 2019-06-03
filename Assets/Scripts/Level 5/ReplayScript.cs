using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ReplayScript : MonoBehaviour
{
    public void MenuButton() {
        PlayerPrefs.SetInt("ScoreCount", 0);
        PlayerPrefs.SetInt("LifeCount", 3);
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }
}

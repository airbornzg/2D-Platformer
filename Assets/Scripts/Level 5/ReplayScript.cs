using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ReplayScript : MonoBehaviour
{
    public Text mText;
    void Start()
    {
        mText.text = PlayerPrefs.GetInt("playerScore").ToString();
    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Menu");
    }
}

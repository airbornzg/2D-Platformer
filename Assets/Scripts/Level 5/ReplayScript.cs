using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ReplayScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Level 5");
    }
}

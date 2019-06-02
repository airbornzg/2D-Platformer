using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingSceneController : MonoBehaviour
{


 
    [SerializeField] float EndLevelLoadingTime;
    [SerializeField] string levelLoader;



    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadScene());

        }
    }


    IEnumerator LoadScene()
    {

        yield return new WaitForSeconds(EndLevelLoadingTime);

        SceneManager.LoadScene(levelLoader);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextController : MonoBehaviour
{

    public Text textfield;
    public string text;
    private Text dreamText;

    // Start is called before the first frame update
    void Start()
    {
        dreamText = GameObject.Find("Dream").GetComponent<Text>();
        StartCoroutine(RemoveAfterSeconds(5, dreamText));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        textfield.text = text;
    }

    IEnumerator RemoveAfterSeconds(int seconds, Text obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.gameObject.SetActive(false);
    }
}

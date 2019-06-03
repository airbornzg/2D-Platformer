using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionScript : MonoBehaviour
{
    [SerializeField]
    private Text textL;
    [SerializeField]
    private Text textM;
    [SerializeField]
    private Text textR;
    [SerializeField]
    private RawImage keyBoard;
    [SerializeField]
    private RawImage mice;

    private float timer = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Instruction());
    }

    IEnumerator Instruction() {
        yield return new WaitForSeconds(timer);
        textL.gameObject.SetActive(false);
        textM.gameObject.SetActive(false);
        textR.gameObject.SetActive(false);
        keyBoard.gameObject.SetActive(false);
        mice.gameObject.SetActive(false);
    }
   
}

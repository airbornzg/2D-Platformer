using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerController : MonoBehaviour
{
    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCrouch();
    }

    private void PlayerCrouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            myAnim.SetBool("Crouched", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            myAnim.SetBool("Crouched", false);
        }
    }
}

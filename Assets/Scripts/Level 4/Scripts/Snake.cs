using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

   
        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            myAnimator.SetBool("PlayerViscinity", true);
        }

        else
        {
            myAnimator.SetBool("PlayerViscinity", false);
        }
        }
    }




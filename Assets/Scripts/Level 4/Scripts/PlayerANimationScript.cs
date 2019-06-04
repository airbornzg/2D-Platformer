using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerANimationScript : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    public Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        HandleInput();
        HandleMovement();
    }

    private void HandleInput()
    {
    if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            myAnimator.SetTrigger("attack");
        }

}
    private void HandleMovement()
    {
        if (myRigidbody2D.velocity.x > 0)
        {
            myAnimator.SetBool("Walk 0", true);

        }
        else
        {
            myAnimator.SetBool("Walk 0", false);
        }
    }


}

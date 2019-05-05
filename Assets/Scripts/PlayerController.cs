using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 10f;

    private Rigidbody2D myRigidbody2d;
    private Animator myAnim;

    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2d = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerJump();
        PlayerAnimationControl();
    }

    private void PlayerAnimationControl()
    {
        myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody2d.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);
    }

    private void PlayerMovement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            myRigidbody2d.velocity = new Vector3(moveSpeed, myRigidbody2d.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            myRigidbody2d.velocity = new Vector3(-moveSpeed, myRigidbody2d.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            myRigidbody2d.velocity = new Vector3(0f, myRigidbody2d.velocity.y, 0f);
        }
    }

    private void PlayerJump()
    {
        isGrounded = myRigidbody2d.velocity.y == 0f;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            myRigidbody2d.velocity = new Vector3(myRigidbody2d.velocity.x, jumpHeight, 0f);
        }
    }
}

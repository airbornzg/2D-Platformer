using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class player5Movement : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 400f;
    [Range(0, 1)] [SerializeField] private float spdOnCrouch = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float movementSmooth = .05f;
    [SerializeField] private LayerMask groundMask;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool onAirControl = false; // allow player to act on air or not
    [SerializeField] private Transform headCheck;
    [SerializeField] private Collider2D disableCollideronCrouch;                // A collider that will be disabled when crouching

    const float groundedRadius = 0.2f; // Radius of the overlap circle to determine if grounded
    private bool onGround;
    const float headRadius = 0.2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D rb;
    private bool playerFacingRight = true;
    private Vector3 vectorRef = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool onCrouch = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool Grounded = onGround;
        onGround = false;


        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                onGround = true;
                if (!Grounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(headCheck.position, headRadius, groundMask))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (onGround || onAirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!onCrouch)
                {
                    onCrouch = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= spdOnCrouch;

                // Disable one of the colliders when crouching
                if (disableCollideronCrouch != null)
                    disableCollideronCrouch.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (disableCollideronCrouch != null)
                    disableCollideronCrouch.enabled = true;

                if (onCrouch)
                {
                    onCrouch = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move  character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            // smoothing the velocity out and apply to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref vectorRef, movementSmooth);

            
            if (move > 0 && !playerFacingRight)
            {
                
                RotatePlayer();
            }
           
            else if (move < 0 && playerFacingRight)
            {
              
                RotatePlayer();
            }
        }
       
        if (onGround && jump)
        {
           
            onGround = false;
            rb.AddForce(new Vector2(0f, jumpHeight));
        }
    }


    private void RotatePlayer()
    {
        
        playerFacingRight = !playerFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}

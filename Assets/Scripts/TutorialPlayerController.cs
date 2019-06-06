using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerController : MonoBehaviour
{
    private Animator myAnim;
    public Transform firePoint;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCrouch();
        PlayerShot();
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

    private void PlayerShot()
    {
        if (Input.GetButtonDown("Fire1")) {
            myAnim.SetBool("Shot", true);
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
        else
        {
            myAnim.SetBool("Shot", false);
        }
    }
}

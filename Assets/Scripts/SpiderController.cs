using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private bool canMove;
    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            myRigidbody2D.velocity = new Vector3(-moveSpeed, myRigidbody2D.velocity.y, 0f);
        }
    }

    private void OnBecameVisible()
    {
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillPlane")
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OozeController : MonoBehaviour
{
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;
    [SerializeField] float moveSpeed;
    [SerializeField] bool movingRight = false;

    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight && transform.position.x > rightPoint.position.x)
        {
            movingRight = false;
        }

        if (!movingRight && transform.position.x < leftPoint.position.x)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            myRigidbody2D.velocity = new Vector3(moveSpeed, myRigidbody2D.velocity.y, 0f);
        }
        else
        {
            myRigidbody2D.velocity = new Vector3(-moveSpeed, myRigidbody2D.velocity.y, 0f);
        }
    }
}

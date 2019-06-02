using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    [SerializeField] Transform upPoint;
    [SerializeField] Transform downtPoint;
    [SerializeField] float moveSpeed;
    [SerializeField] bool movingDown = false;

    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovingBoundary();

        MovingSpeed();
    }

    private void MovingSpeed()
    {
        if (movingDown)
        {
            myRigidbody2D.velocity = new Vector3(myRigidbody2D.velocity.x, moveSpeed, 0f);
        }
        else
        {
            myRigidbody2D.velocity = new Vector3(-myRigidbody2D.velocity.x, -moveSpeed, 0f);
        }
    }

    private void MovingBoundary()
    {
        if (movingDown && transform.position.y > downtPoint.position.x)
        {
            movingDown = true;
        }

        if (!movingDown && transform.position.y < upPoint.position.x)
        {
            movingDown = false;
        }
    }
}

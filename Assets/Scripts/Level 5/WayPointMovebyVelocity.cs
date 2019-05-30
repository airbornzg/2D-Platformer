using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovebyVelocity : MonoBehaviour
{
    public float spd = 5.0f;
    public GameObject[] checkPoint;
    int counter = 0;
    public float distance = 2.0f;

    void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;

        direction = checkPoint[counter].transform.position - transform.position;

        if (direction.magnitude < distance) {
            if (counter < checkPoint.Length - 1)
            {
                counter++;
            }
            else {
                counter = 0;
            }
            
        }
        direction = direction.normalized;
        Vector3 dir = direction;
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * spd, direction.y * spd);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnWayPoint : MonoBehaviour
{
    [SerializeField]
    Transform[] wayPoint; // set of waypoiont for move

    [SerializeField]
    float moveSpd = 2.0f;

    int indexOfWaypoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = wayPoint[indexOfWaypoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint[indexOfWaypoint].transform.position, moveSpd * Time.deltaTime);

        if (transform.position == wayPoint[indexOfWaypoint].position) {
            indexOfWaypoint += 1;
        }
        if (indexOfWaypoint == wayPoint.Length) {
            indexOfWaypoint = 0;
        }
    }
}

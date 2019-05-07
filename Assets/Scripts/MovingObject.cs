using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] GameObject objectToMove;

    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;

    [SerializeField] float moveSpeed;

    private Vector3 currentTartget;

    // Start is called before the first frame update
    void Start()
    {
        currentTartget = endPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTartget, moveSpeed * Time.deltaTime);

        if (objectToMove.transform.position == endPoint.position)
        {
            currentTartget = startPoint.position;
        }

        if (objectToMove.transform.position == startPoint.position)
        {
            currentTartget = endPoint.position;
        }
    }
}

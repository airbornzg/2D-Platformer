using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResetOnRespawn : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRotation;
    private Vector3 startLocalScale;

    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
        startLocalScale = transform.localScale;

        if(GetComponent<Rigidbody2D>() != null)
        {
            myRigidbody2D = GetComponent<Rigidbody2D>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetObject()
    {
        transform.position = startPos;
        transform.rotation = startRotation;
        transform.localScale = startLocalScale;

        if (myRigidbody2D != null)
        {
            myRigidbody2D.velocity = Vector3.zero;
        }
    }
}

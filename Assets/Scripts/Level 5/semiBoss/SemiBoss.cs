using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiBoss : MonoBehaviour
{
    private ISemiBoss currentState;
    public Animator s_anim;
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new IdleState()); // set the first state is idle
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute();
    }

    public void ChangeState(ISemiBoss newState) {
        if (currentState != null) {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);

    }
}

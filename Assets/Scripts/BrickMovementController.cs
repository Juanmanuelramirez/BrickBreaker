using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMovementController : MonoBehaviour
{
    public enum brickState
    {
        stop,
        move
    }

    public brickState currentState;

    private bool hasMove;

    // Start is called before the first frame update
    void Start()
    {
        hasMove = false;
        currentState = brickState.stop;    
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == brickState.stop)
        {
            hasMove = false;
        }
        if (currentState == brickState.move && hasMove == false)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            currentState = brickState.stop;
            hasMove = true;
        }
    }
}

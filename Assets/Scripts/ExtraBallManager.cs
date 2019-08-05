using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraBallManager : MonoBehaviour
{
    private BallController ballController;
    private GameManager gameManager;
    public float ballWaitTime;
    private float ballWaitTimeSeconds;
    public int numberOfExtraBalls;
    public int numberOfBallsToFire;
    public ObjectPool objectPool;
    public Text numberOfBallText;


    // Start is called before the first frame update
    void Start()
    {
        ballController = FindObjectOfType<BallController>();
        gameManager = FindObjectOfType<GameManager>();
        ballWaitTimeSeconds = ballWaitTime;
        numberOfExtraBalls = 0;
        numberOfBallsToFire = 0;
        numberOfBallText.text = ""+1;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfBallText.text = "" + numberOfExtraBalls;
        if (ballController.currentBallState == BallController.ballState.fire)
        {
            if (numberOfBallsToFire > 0)
            {
                ballWaitTimeSeconds -= Time.deltaTime;
                if (ballWaitTimeSeconds <= 0)
                {
                    GameObject ball = objectPool.GetPooledObject("ExtraBall");
                    if (ball != null)
                    {
                        ball.transform.position = ballController.ballLaunchPosition;
                        ball.SetActive(true);
                        gameManager.ballsScene.Add(ball);
                        ball.GetComponent<Rigidbody2D>().velocity = -ballController.constantSpeed * ballController.tempVelocity;
                        ballWaitTimeSeconds = ballWaitTime;
                        numberOfBallsToFire--;
                    }
                    ballWaitTimeSeconds = ballWaitTime;
                }
            }
        }
        if(ballController.currentBallState == BallController.ballState.endShot)
        {
            numberOfBallsToFire = numberOfExtraBalls;
        }
    }
}

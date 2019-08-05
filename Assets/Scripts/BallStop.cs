using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStop : MonoBehaviour
{
    public GameObject ball;
    public BallController ballControl;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ball")
        {
            //parar la pelota
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ball.transform.position = new Vector3(ball.transform.position.x, -4.221f, ball.transform.position.z);
            //rotar la pelota
            ballControl.currentBallState = BallController.ballState.wait;
            ball.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            //resetear el nivel
            //poner la pelota activa una ves más
        }
        if(other.gameObject.tag == "ExtraBall")
        {
            gameManager.ballsScene.Remove(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}

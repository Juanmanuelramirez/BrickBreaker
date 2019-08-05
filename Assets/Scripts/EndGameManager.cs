using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    private BallController ball;
    public GameObject endGamePanel;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<BallController>();
        endGamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Square Brick" 
            || collision.tag == "Triangle Brick")
        {
            ball.currentBallState = BallController.ballState.endGame;
            endGamePanel.SetActive(true);
        }
    }
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

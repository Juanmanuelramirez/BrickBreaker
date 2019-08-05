using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public enum ballState
    {
        aim,
        fire,
        wait,
        endShot,
        endGame
    }

    public ballState currentBallState;

    public GameObject ball;
    private Vector2 mouseStartPosition;
    private Vector2 mouseEndPosition;
    public Vector3 ballLaunchPosition;
    private float ballVelocityX;
    private float ballVelocityY;
    public float constantSpeed;
    public GameObject arrow;

    private Vector2 iniBallPos;
    public Vector2 tempVelocity;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        iniBallPos = this.transform.position;
        currentBallState = ballState.aim;
        gameManager.ballsScene.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentBallState)
        {
            case ballState.aim:
                if (Input.GetMouseButtonDown(0))
                {
                    MouseClicked();
                }
                if (Input.GetMouseButton(0))
                {
                    MouseDraged();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    ReleaseMouse();
                }
                break;
            case ballState.fire:

                break;
            case ballState.wait:
                //
                if (gameManager.ballsScene.Count == 1)
                {
                    currentBallState = ballState.endShot;
                }
                break;
            case ballState.endShot:
                for(int i=0; i<gameManager.bricksInScene.Count; i++)
                {
                    gameManager.bricksInScene[i].GetComponent<BrickMovementController>().currentState = BrickMovementController.brickState.move;
                }
                gameManager.PlaceBricks();
                currentBallState = ballState.aim;
                break;
            case ballState.endGame:

                break;
            default:

                break;
        }
    }
     
    public void MouseClicked()
    {
        arrow.SetActive(true);
        //obtenemos la posicion del mouse al presionar el boton
        //derecho del mouse y lo transformamos a las coordenadas
        //del la escena
        mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void MouseDraged()
    {
        //mover la flecha
        arrow.SetActive(true);
        Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Calculamos el angulo de la flecha
        float diffX = iniBallPos.x - tempMousePosition.x;
        float diffY = iniBallPos.y - tempMousePosition.y;
        //Evitamos el erro de división entre cero 
        //Obtenemos con la funcion de arco tangente los grado en radianes
        //Los tranformamos a grados para dar la rotación
        float theta = Mathf.Rad2Deg * Mathf.Atan(diffX / diffY);
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, -theta);
    }

    public void ReleaseMouse()
    {
        arrow.SetActive(false);
        //Obtenemos la posición del mouse al momento de levantar el botón
        //Esta posición será con respecto a la posición de la pantalla
        mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Ontenemos la distancia entre los puntos X y Y 
        ballVelocityX = (iniBallPos.x - mouseEndPosition.x);
        ballVelocityY = (iniBallPos.y - mouseEndPosition.y);
        //Damos la velocidad dependiendo del vector normalizado de las distancias
        tempVelocity = new Vector2(ballVelocityX, ballVelocityY).normalized;
        //Damos la velocidad a la bola 
        ball.GetComponent <Rigidbody2D>().velocity = -constantSpeed * tempVelocity;
        if(ball.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            return;
        }

        ballLaunchPosition = transform.position;
        //Pasamos al estado fire
        currentBallState = ballState.fire;
    }
}

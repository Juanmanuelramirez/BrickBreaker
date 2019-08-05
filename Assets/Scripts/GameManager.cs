using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] Bricks;
    public GameManager extraballPowerup;
    public int numberOfBricksToStart;
    private int dificultyBrick;

    public int level;

    public List<GameObject> bricksInScene;
    //Lista de pelotas en escena
    public List<GameObject> ballsScene;

    private ObjectPool objectPool;

    public int numberOfExtraBallsInRow;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        
        level = 1; 
        numberOfBricksToStart = Random.Range(dificultyBrick, 7);
        for (int i=0;i< numberOfBricksToStart; i++)
        {
            int brickToCreate = Random.Range(0, 3);

            if(brickToCreate == 2 && numberOfExtraBallsInRow == 0) { 
                bricksInScene.Add(
                Instantiate(Bricks[brickToCreate], spawnPoints[i].position, 
                Quaternion.identity)
                );
                numberOfExtraBallsInRow++;
            }
            else
            {
                bricksInScene.Add(
                Instantiate(Bricks[brickToCreate], spawnPoints[i].position,
                Quaternion.identity)
                );
            }
        }
        numberOfExtraBallsInRow = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceBricks()
    {
        level++;

        //iteramos todas las posiciones de spawnPonts
        //le damos de manera aleatoria un ladrillo 
        foreach (Transform position in spawnPoints)
        {
            int bricksToCreate = Random.Range(0, 3);
            if (bricksToCreate == 0)
            {
                CreateBrick("Square Brick", position);
                
            }
            else if (bricksToCreate == 1)
            {
                //Regresa el elemento que se mostrará en escena
                CreateBrick("Triangle Brick", position);
            }
            else if (bricksToCreate == 2 && numberOfExtraBallsInRow == 0)
            {
                //Regresa el elemento que se mostrará en escena
                CreateBrick("Triangle Brick", position);
                numberOfExtraBallsInRow++;
            }
        }
        numberOfExtraBallsInRow = 0;
    }

    public void CreateBrick(string stringBrick, Transform spawnBrick )
    {
        GameObject brick;
        brick = objectPool.GetPooledObject(stringBrick);
        bricksInScene.Add(brick);

        if (brick != null)
        {
            brick.transform.position = spawnBrick.position;
            brick.transform.rotation = Quaternion.identity;
            brick.SetActive(true);
        }
    }
}

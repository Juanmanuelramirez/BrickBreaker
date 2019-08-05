using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickHealthManager : MonoBehaviour
{
    public int brickHeatth;
    private Text brickHealthText;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        brickHeatth = gameManager.level;
        brickHealthText = GetComponentInChildren<Text>();
    }

    void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        brickHeatth = gameManager.level;
    }

    // Update is called once per frame
    void Update()
    {
        brickHealthText.text = "" + brickHeatth;
        if(brickHeatth <= 0)
        {
            //Destruir ladrillo
            this.gameObject.SetActive(false); 
        }
    }

    void takeDamage(int damageToTake)
    {
        brickHeatth -= damageToTake;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ball"
             || other.gameObject.tag == "ExtraBall")
        {
            takeDamage(1);
        }
    }
}

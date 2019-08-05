using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBall : MonoBehaviour
{
    private ExtraBallManager extraBallManager;

    private void Start()
    {
        extraBallManager = FindObjectOfType<ExtraBallManager>();
    }

    //Instanciamos una bola nueva al momento de chocar con el power-up
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" 
            || collision.gameObject.tag == "ExtraBall")
        {
            //add an extra ball 
            extraBallManager.numberOfExtraBalls++;
            this.gameObject.SetActive(false);
        }
    }
}

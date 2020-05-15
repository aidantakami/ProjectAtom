using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{

    public GameManager gameManager;

    //Used to end the game via the GameManager
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("DogPlayer"))
        {
            gameManager.EndGame();
        }
        else if (other.gameObject.CompareTag("BoomerangPlayer"))
        {
            gameManager.BoomerangDead();
        }
    }
}

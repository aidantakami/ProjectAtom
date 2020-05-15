using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameTrigger : MonoBehaviour
{

    [SerializeField] UnityEvent dogCollided = new UnityEvent();
    [SerializeField] UnityEvent boomCollided = new UnityEvent();

    //Used to end the game via the GameManager
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("DogPlayer"))
        {
            dogCollided.Invoke();
        }
        else if (other.gameObject.CompareTag("BoomerangPlayer"))
        {
            boomCollided.Invoke();
        }
    }
}

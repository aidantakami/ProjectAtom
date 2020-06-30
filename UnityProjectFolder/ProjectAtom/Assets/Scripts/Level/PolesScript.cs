using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PolesScript : MonoBehaviour
{
    [SerializeField] GameObject ropes;
    [SerializeField] public UnityEvent DogCollided = new UnityEvent ();

    private bool ropesDestroyed = false;
    private bool playerIsPastChunk = false;

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag ("DogPlayer")) DogCollided.Invoke ();

        else if (other.gameObject.CompareTag ("BoomerangPlayer"))
        {
            ropes.gameObject.SetActive (false);
            ropesDestroyed = true;
        }
    }

    public void ChunkLoadAfterRopesHit ()
    {
        if (ropesDestroyed)
        {
            if (playerIsPastChunk)
            {
                ropes.gameObject.SetActive (true);
                playerIsPastChunk = false;
                ropesDestroyed = false;
            }
            else
            {
                playerIsPastChunk = true;
            }
        }

    }
}
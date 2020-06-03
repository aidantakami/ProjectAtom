using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DummyEnemy : MonoBehaviour, IEnemy
{

    [SerializeField] UnityEvent dogCollided = new UnityEvent ();
    [SerializeField] public EnemyManager enemyManager;
    [SerializeField] public ParticleSystem enemyParticles;
    private ParticleSystem myParticles;

    // Start is called before the first frame update
    void Start ()
    {
        enemyManager.AddToEnemyManager (gameObject);

    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void EnemyPause ()
    {

    }

    public void KillEnemy ()
    {
        if (myParticles == null)
        {
            myParticles = Instantiate (enemyParticles);
        }

        myParticles.transform.position = transform.position;
        myParticles.Play ();
        enemyManager.SetEnemyInactive (gameObject);
    }

    //When enemy is hit by something
    private void OnCollisionEnter (Collision other)
    {
        //If player
        if (other.gameObject.CompareTag ("DogPlayer"))
        {
            //Do Something
            dogCollided.Invoke ();

        }
        else if (other.gameObject.CompareTag ("BoomerangPlayer"))
        {
            KillEnemy ();
        }

    }
}
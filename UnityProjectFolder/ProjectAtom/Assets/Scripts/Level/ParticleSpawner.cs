using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{

    [SerializeField] public ParticleSystem boomMagnetPS;
    [SerializeField] public Vector3Variable boomPos;
    [SerializeField] public Vector3Variable dogPos;
    // Start is called before the first frame update
    void Start ()
    {
        boomMagnetPS.Stop ();
    }

    // Update is called once per frame
    void Update ()
    {

    }

    //Response to the Magnet ability of the Dog
    public void BoomMagnetSpawn ()
    {
        //Moves & plays PS
        boomMagnetPS.transform.position = boomPos.value;
        boomMagnetPS.Play ();
    }

}
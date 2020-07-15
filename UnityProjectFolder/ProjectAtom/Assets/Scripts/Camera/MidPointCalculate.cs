using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidPointCalculate : MonoBehaviour
{
    [SerializeField] public Vector3Variable dogPlayer;
    [SerializeField] public Vector3Variable boomPlayer;
    [SerializeField] public Vector3Variable midPoint;
    [SerializeField] public BoolVariable isPaused;
    public float cameraUpDownMax;

    private float distanceTemp;
    private float cameraDistanceModifier = 0.5f;
    private Vector3 startingPos;
    private Vector3 temp;
    private float cameraUpDownIterator = 0;
    private bool cameraMovingUp = true;
    private bool gameEnded = false;

    private void Start ()
    {
        startingPos = transform.position;
    }

    void Update ()
    {
        if (!isPaused.value && !gameEnded)
        {
            //Gets mid point of the dog and boomerang
            temp = ((dogPlayer.value + boomPlayer.value) / 2) + (Vector3.up * cameraUpDownIterator);

            //Finds another midpoint between that point and dog player
            midPoint.value = (temp + dogPlayer.value) / 2;

            //Gets distance between 2 players
            distanceTemp = Vector3.Distance (dogPlayer.value, boomPlayer.value);

            midPoint.SetValue (new Vector3 (midPoint.X, midPoint.Y, midPoint.Z - (distanceTemp * cameraDistanceModifier)));

            //Move camera up and down      
            if ((cameraUpDownIterator <= cameraUpDownMax) && cameraMovingUp)
            {
                cameraUpDownIterator += 0.005f;
            }
            else if ((cameraUpDownIterator > 0) && !cameraMovingUp)
            {
                cameraUpDownIterator -= 0.005f;
            }
            else if ((cameraUpDownIterator > cameraUpDownMax) && cameraMovingUp)
            {
                cameraMovingUp = false;
            }
            else if ((cameraUpDownIterator < 0) && !cameraMovingUp)
            {
                cameraMovingUp = true;
            }
        }
    }

    public void MidpointRestart ()
    {
        midPoint.value = startingPos;
        gameEnded = false;
    }

    public void MidpointGameStart ()
    {
        MidpointRestart ();
    }

    //stops moving the MP & camera
    public void MidpointEndGame ()
    {
        gameEnded = true;
    }
}
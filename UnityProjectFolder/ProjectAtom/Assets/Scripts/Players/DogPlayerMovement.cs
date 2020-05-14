﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPlayerMovement : MonoBehaviour
{

    //Serialized fields
    [SerializeField] public FloatReference playerSpeed;
    [SerializeField] public Vector3Variable dogLocation;
    [SerializeField] public Vector3Variable boomLocation;
    [SerializeField] public BoomerangPlayerMovement boomPlayer;
    [SerializeField] public Transform leftLimit;
    [SerializeField] public Transform rightLimit;
    [SerializeField] public GameObject aimArrow;



    //private fields
    private Vector3 startingPosition;

    //Referencing Components
    private Rigidbody rb;

    //State Machine & Awake
#region
    //State machine and states
    private StateMachine _StateMachine;
    private ISDogRunning DogRunningState;
    private ISDogNoBoom DogNoBoomState;
    private ISStandby DogStandbyState;

    public void Awake()
    {
        _StateMachine = new StateMachine();
        StateConstructor();

        //Used to restart player
        startingPosition = transform.position;
    }

    private void StateConstructor()
    {
        DogRunningState = new ISDogRunning(dogLocation, playerSpeed, this);
        DogNoBoomState = new ISDogNoBoom(dogLocation, boomLocation, playerSpeed, this);
        DogStandbyState = new ISStandby(dogLocation, gameObject.transform);
    }
#endregion


    //Game Manager Responses
#region
    //Called when game is paused
    public void DogGamePause()
    {
        _StateMachine.EnterState(DogStandbyState);
    }

    //Called when game is unpaused
    public void DogGameUnpause()
    {
        _StateMachine.ReturnToPreviousState();
    }

    //Called when the dog begins to run, after both players signed in
    public void DogGameStart()
    {
        DogGameRestart();
        _StateMachine.EnterState(DogRunningState);
    }

    public void DogGameRestart()
    {
        transform.position = startingPosition;
        aimArrow.SetActive(true);
        _StateMachine.EnterState(DogRunningState);
    }

    public void DogGameEnd()
    {
        _StateMachine.EnterState(DogStandbyState);
    }
#endregion

    // Update is called once per frame
    private void Update() => _StateMachine.Tick();

    // Start is called before the first frame update
    void Start()
    {
        _StateMachine.EnterState(DogStandbyState);
        rb = gameObject.GetComponent<Rigidbody>();
        aimArrow.SetActive(true);
    }

    //Functions
#region

    //When boomerang is thrown
    public void BoomerangThrown(Vector3 aimLocation)
    {
        //Tell boomerang
        boomPlayer.BoomerangThrownAction(aimLocation);

        //Change states
        _StateMachine.EnterState(DogNoBoomState);
        aimArrow.SetActive(false);
    }

    public void TryToCatchBoomerang()
    {
        if (boomPlayer.TryToGetCaught())
        {
            _StateMachine.EnterState(DogRunningState);
            aimArrow.SetActive(true);

        }
    }


    //Used to access the limits of the aim set in editor
    public Vector3 GetLeftAimLimit()
    {
        return leftLimit.position;
    }

    public Vector3 GetRightAimLimit()
    {
        return rightLimit.position;
    }

    public GameObject GetAimIndicator()
    {
        return aimArrow;
    }

    #endregion

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISDogRunning : IState
{
    protected Vector3Variable _dogLocation;
    protected FloatVariable _playerSpeed;
    protected BoolVariable _playerCanMove;
    protected DogPlayerMovement _player;

    private readonly float horizontalMovementMod = 18;
    private Vector3 aimLocation = new Vector3 ();
    private GameObject arrowGO;
    private GameObject aimPointGO;
    private float horizontalDampTemp;
    private float lastFrameInput;
    private float rotationTempOld = 0;
    private float rotationTempNew = 0;
    private float tempHorizontalInput = 0;

    public ISDogRunning (Vector3Variable dogLocation, FloatVariable playerSpeed, BoolVariable playerCanMove, DogPlayerMovement player)
    {
        _dogLocation = dogLocation;
        _playerSpeed = playerSpeed;
        _playerCanMove = playerCanMove;
        _player = player;
    }

    public void OnStateEnter ()
    {
        arrowGO = _player.GetAimArrow ();
        aimPointGO = _player.GetAimPoint ();

        _player.ResetRotationOfDog ();

    }

    public void OnStateExit ()
    {

    }

    public void OnStateTick ()
    {
        if (_playerCanMove.value)
        {
            //Moves player forwards
            _player.transform.Translate (Vector3.forward * Time.deltaTime * _playerSpeed);

            tempHorizontalInput = Input.GetAxis ("P1Left Stick Horizontal");
            horizontalDampTemp = Mathf.Lerp (lastFrameInput, tempHorizontalInput, 0.25f);

            if (tempHorizontalInput > 0)
            {
                rotationTempNew = Mathf.Lerp (0, 15, horizontalDampTemp);;
            }
            else if (tempHorizontalInput < 0)
            {
                rotationTempNew = Mathf.Lerp (0, -15, -horizontalDampTemp);;
            }
            else
            {
                rotationTempNew = 0;
                _player.ResetRotationOfDog ();

            }

            _player.transform.Rotate (0, rotationTempNew - rotationTempOld, 0);

            rotationTempOld = rotationTempNew;

            //Allows player to move LR
            _player.transform.Translate (Vector3.right * Time.deltaTime * horizontalDampTemp * horizontalMovementMod);
            lastFrameInput = tempHorizontalInput;

            //Throw boomerang when A pressed
            if (Input.GetButtonDown ("P1A Button") || Input.GetKeyDown (KeyCode.E))
            {
                //Tells player to throw
                _player.BoomerangThrown (aimLocation);
            }

            //Reads for player using ability
            if (Input.GetButtonDown ("P1B Button") || Input.GetKeyDown (KeyCode.R))
            {
                _player.UseSelectedDogAbility ();
            }

            //Allows player to switch through abilities
            if (Input.GetButtonDown ("P1X Button") || Input.GetKeyDown (KeyCode.F))
            {
                _player.SwitchDogAbility ();
            }
        }

        //Gets player's aim
        //Axis is between -1 and 1, adding 1 and then dividing by 2 to get complete input
        aimLocation = Vector3.Lerp (_player.GetLeftAimLimit (), _player.GetRightAimLimit (), ((Input.GetAxis ("P2Left Stick Horizontal")) + 1) / 2);

        //Will rotate arrow assigned to plauer
        arrowGO.transform.LookAt (aimLocation);

        //Puts aim point at aim locations
        aimPointGO.transform.position = aimLocation;

        //Updates player's location
        _dogLocation.value = _player.transform.position;
    }
}
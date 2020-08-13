using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISDogBoomDead : IState
{

    private Vector3Variable _dogLocation;
    private FloatVariable _playerSpeed;
    private BoolVariable _playerCanMove;
    private DogPlayerMovement _player;

    private readonly float horizontalMovementMod = 18;
    private float horizontalDampTemp;
    private float lastFrameInput;
    private float tempHorizontalInput = 0;
    private float rotationTempOld = 0;
    private float rotationTempNew = 0;

    public ISDogBoomDead (Vector3Variable dogLocation, FloatVariable playerSpeed, BoolVariable playerCanMove, DogPlayerMovement player)
    {
        _dogLocation = dogLocation;
        _playerSpeed = playerSpeed;
        _playerCanMove = playerCanMove;
        _player = player;
    }

    public void OnStateEnter ()
    {
        _player.ResetRotationOfDog ();

    }

    public void OnStateExit ()
    {

    }

    public void OnStateTick ()
    {

        if (_playerCanMove.value)
        {
            tempHorizontalInput = Input.GetAxis ("P1Left Stick Horizontal");

            //Moves player forwards
            _player.transform.Translate (Vector3.forward * Time.deltaTime * _playerSpeed);

            //Allows player to move LR
            _player.transform.Translate (Vector3.right * Time.deltaTime * horizontalDampTemp * horizontalMovementMod);
            lastFrameInput = tempHorizontalInput;

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

        //Updates player's location
        _dogLocation.value = _player.transform.position;
    }

}
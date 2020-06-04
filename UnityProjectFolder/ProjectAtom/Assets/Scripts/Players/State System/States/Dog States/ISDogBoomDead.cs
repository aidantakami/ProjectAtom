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

    public ISDogBoomDead (Vector3Variable dogLocation, FloatVariable playerSpeed, BoolVariable playerCanMove, DogPlayerMovement player)
    {
        _dogLocation = dogLocation;
        _playerSpeed = playerSpeed;
        _playerCanMove = playerCanMove;
        _player = player;
    }

    public void OnStateEnter ()
    {

    }

    public void OnStateExit ()
    {

    }

    public void OnStateTick ()
    {

        Debug.Log ("you found it");

        if (_playerCanMove.value)
        {
            //Moves player forwards
            _player.transform.Translate (Vector3.forward * Time.deltaTime * _playerSpeed);

            horizontalDampTemp = Mathf.Lerp (lastFrameInput, Input.GetAxis ("P1Left Stick Horizontal"), 0.25f);

            //Allows player to move LR
            _player.transform.Translate (Vector3.right * Time.deltaTime * horizontalDampTemp * horizontalMovementMod);
            lastFrameInput = Input.GetAxis ("P1Left Stick Horizontal");

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
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISDogBoomDead : IState
{

    private Vector3Variable _dogLocation;
    private FloatVariable _playerSpeed;
    private BoolVariable _playerCanMove;
    private DogPlayerMovement _player;

    private readonly float horizontalMovementMod = 5;


    public ISDogBoomDead(Vector3Variable dogLocation, FloatVariable playerSpeed, BoolVariable playerCanMove, DogPlayerMovement player)
    {
        _dogLocation = dogLocation;
        _playerSpeed = playerSpeed;
        _playerCanMove = playerCanMove;
        _player = player;
    }

    public void OnStateEnter()
    {

    }

    public void OnStateExit()
    {

    }

    public void OnStateTick()
    {
        if (_playerCanMove.value)
        {
            //Moves player forwards
            _player.transform.Translate(Vector3.forward * Time.deltaTime * _playerSpeed);

            //Allows player to move LR
            _player.transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("P1Left Stick Horizontal") * horizontalMovementMod);

            
        }

        //Updates player's location
        _dogLocation.value = _player.transform.position;
    }

}

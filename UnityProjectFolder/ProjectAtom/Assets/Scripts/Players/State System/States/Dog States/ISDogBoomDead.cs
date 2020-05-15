using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISDogBoomDead : IState
{

    private Vector3Variable _dogLocation;
    private float _playerSpeed;
    private DogPlayerMovement _player;

    private readonly float horizontalMovementMod = 3;


    public ISDogBoomDead(Vector3Variable dogLocation, FloatReference playerSpeed, DogPlayerMovement player)
    {
        _dogLocation = dogLocation;
        _playerSpeed = playerSpeed.Value;
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
        //Moves player forwards
        _player.transform.Translate(Vector3.forward * Time.deltaTime * _playerSpeed);

        //Allows player to move LR
        _player.transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("P1Left Stick Horizontal") * horizontalMovementMod);

        //Updates player's location
        _dogLocation.value = _player.transform.position;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISDogNoBoom : IState
{
    protected Vector3Variable _dogLocation;
    protected Vector3Variable _boomLocation;
    protected FloatVariable _playerSpeed;
    protected BoolVariable _playerCanMove;
    protected DogPlayerMovement _player;

    private readonly float horizontalMovementMod = 5;

    public ISDogNoBoom(Vector3Variable dogLocation, Vector3Variable boomLocation, BoolVariable playerCanMove, FloatVariable playerSpeed, DogPlayerMovement player)
    {
        _dogLocation = dogLocation;
        _boomLocation = boomLocation;
        _playerCanMove = playerCanMove;
        _playerSpeed = playerSpeed;
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

            if (Physics.Raycast(_player.transform.position, _player.transform.TransformDirection(Vector3.down), 1f))
            {
                //Allows player to move side to side
                _player.transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("P1Left Stick Horizontal") * horizontalMovementMod);
            }

            //Updates player's location
            _dogLocation.value = _player.transform.position;

            if (Input.GetButtonDown("P1A Button") || Input.GetKeyDown(KeyCode.E))
            {
                if (Vector3.Distance(_dogLocation.value, _boomLocation.value) < 3f)
                {
                    _player.TryToCatchBoomerang();
                }

            }
        }
    }        
}

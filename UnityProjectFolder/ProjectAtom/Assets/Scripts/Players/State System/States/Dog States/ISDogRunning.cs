using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISDogRunning : IState
{
    protected Vector3Variable _dogLocation;
    protected float _playerSpeed;
    protected DogPlayerMovement _player;

    private readonly float horizontalMovementMod = 3;


    public ISDogRunning(Vector3Variable dogLocation, FloatReference playerSpeed, DogPlayerMovement player)
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
        
        //Throw boomerang when A pressed
        if(Input.GetButtonDown("P1A Button"))
        {
            //Tells player to throw
            _player.BoomerangThrown();
        }

        //Updates player's location
        _dogLocation.value = _player.transform.position;
    }
}

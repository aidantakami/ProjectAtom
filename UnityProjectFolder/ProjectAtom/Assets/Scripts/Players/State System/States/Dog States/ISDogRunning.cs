using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISDogRunning : IState
{
    protected Vector3Variable _dogLocation;
    protected float _playerSpeed;
    protected DogPlayerMovement _player;

    private readonly float horizontalMovementMod = 3;
    private Vector3 aimLocation = new Vector3();


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


        //Gets player's aim
        //Axis is between -1 and 1, adding 1 and then dividing by 2 to get complete input
        aimLocation = Vector3.Lerp(_player.GetLeftAimLimit(), _player.GetRightAimLimit(), ((Input.GetAxis("P1Right Stick Horizontal")) + 1) / 2);
        Debug.DrawRay(_player.transform.position, aimLocation, Color.white);

        //Throw boomerang when A pressed
        if(Input.GetButtonDown("P1A Button") || Input.GetKeyDown(KeyCode.E))
        {
            //Tells player to throw
            _player.BoomerangThrown(aimLocation);
        }


        //Updates player's location
        _dogLocation.value = _player.transform.position;
    }
}

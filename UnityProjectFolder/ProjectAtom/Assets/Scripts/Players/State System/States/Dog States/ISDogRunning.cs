using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISDogRunning : IState
{
    protected Vector3Variable _dogLocation;
    protected FloatVariable _playerSpeed;
    protected BoolVariable _playerCanMove;
    protected DogPlayerMovement _player;

    private readonly float horizontalMovementMod = 18;
    private Vector3 aimLocation = new Vector3();
    private GameObject arrowGO;


    public ISDogRunning(Vector3Variable dogLocation, FloatVariable playerSpeed, BoolVariable playerCanMove, DogPlayerMovement player)
    {
        _dogLocation = dogLocation;
        _playerSpeed = playerSpeed;
        _playerCanMove = playerCanMove;
        _player = player;
    }


    public void OnStateEnter()
    {
        arrowGO = _player.GetAimIndicator();
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

            //Throw boomerang when A pressed
            if (Input.GetButtonDown("P1A Button") || Input.GetKeyDown(KeyCode.E))
            {
                //Tells player to throw
                _player.BoomerangThrown(aimLocation);
            }
        }
        


        //Gets player's aim
        //Axis is between -1 and 1, adding 1 and then dividing by 2 to get complete input
        aimLocation = Vector3.Lerp(_player.GetLeftAimLimit(), _player.GetRightAimLimit(), ((Input.GetAxis("P2Left Stick Horizontal")) + 1) / 2);
       
        
        //Will rotate arrow assigned to plauer
        arrowGO.transform.LookAt(aimLocation);

        //Updates player's location
        _dogLocation.value = _player.transform.position;
    }
}

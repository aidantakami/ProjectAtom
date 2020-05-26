using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISBoomThrown : IState
{
    protected Vector3Variable _boomLocation;
    protected Vector3Variable _dogLocation;
    protected FloatVariable _playerSpeed;
    protected BoolVariable _playerCanMove;
    protected BoomerangPlayerMovement _player;

    private readonly float horizontalMovementMod = 18f;
    private readonly float verticallMovementMod = 14f;

    private Ray groundCheck;

    protected MeshRenderer _playerMR;
    protected BoxCollider _playerBC;

    //Constructor
    public ISBoomThrown(Vector3Variable boomLocation, Vector3Variable dogLocation, BoolVariable playerCanMove, FloatVariable playerSpeed, BoomerangPlayerMovement player)
    {
        _boomLocation = boomLocation;
        _dogLocation = dogLocation;
        _playerSpeed = playerSpeed;
        _playerCanMove = playerCanMove;
        _player = player;
    }

   
    public void OnStateEnter()
    {
        //Will detect for walls or collidables to side
        LayerMask mask = LayerMask.GetMask("Collidable");
        RaycastHit hit;
        if (Physics.Raycast(_player.transform.position, _player.transform.TransformDirection(Vector3.right), out hit, 2f, mask))
        {
            //Move Boomerang away from dog
            _player.transform.position += new Vector3(-1, 0.5f, 0);
        }
        else _player.transform.position += new Vector3(1, 0.5f, 0);


        //Get Mesh and Collider
        _playerMR = _player.GetComponent<MeshRenderer>();
        _playerBC = _player.GetComponent<BoxCollider>();


        //Enables Mesh Renderer
        _playerMR.GetComponent<MeshRenderer>().enabled = true;
        //Box collider enabled in IEnumerator in BoomerangPlayerMovement
        
    }


    public void OnStateExit()
    {
        
    }

    public void OnStateTick()
    {
        if (_playerCanMove.value)
        {
            //Moves player forward
            _player.transform.Translate(Vector3.forward * Time.deltaTime * _playerSpeed, Space.World);

            //Input variables
            float x = Input.GetAxis("P2Left Stick Horizontal");
            float y = Input.GetAxis("P2Left Stick Vertical");


            if (_player.BoomIsInRange())
            {
                //Allows player to move L and R
                _player.transform.Translate((Vector3.right * Time.deltaTime * x * horizontalMovementMod), Space.World);
                _player.transform.Translate(Vector3.back * Time.deltaTime * y * verticallMovementMod, Space.World);

                //Keyboard input accessib;e
                if (Input.GetButtonDown("P2B Button") || Input.GetKeyDown(KeyCode.Return))
                {
                    _player.PlaceSpringboard();
                }

            }
            else
            {
                Debug.Log("Not in range");
            }
        }
        
        //Set player location
        _boomLocation.SetValue(_player.transform.position);

    }

}

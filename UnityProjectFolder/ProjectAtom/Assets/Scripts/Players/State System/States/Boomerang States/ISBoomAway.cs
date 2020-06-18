using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISBoomAway : IState
{
    private Vector3Variable _dogLocation;
    private Vector3Variable _boomLocation;
    private BoomerangPlayerMovement _player;

    public ISBoomAway(Vector3Variable boomLocation, Vector3Variable dogLocation, BoomerangPlayerMovement player)
    {
        _boomLocation = boomLocation;
        _dogLocation = dogLocation;
        _player = player;
    }

    public void OnStateEnter()
    {
        _player.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        _player.GetComponent<BoxCollider>().enabled = false;
        
    }

    public void OnStateExit()
    {
        Debug.Log("Boomer State Left");
    }

    public void OnStateTick()
    {
        _player.transform.position = _dogLocation.value;        
        _boomLocation.value = _player.transform.position;

        if (Input.GetButtonDown("P2X Button") || Input.GetKeyDown(KeyCode.P))
        {
            _player.SwitchBoomAbility();
        }
    }
}

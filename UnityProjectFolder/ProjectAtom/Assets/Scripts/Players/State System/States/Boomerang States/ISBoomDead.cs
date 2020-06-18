using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISBoomDead : IState
{
    private Vector3Variable _boomLocation;
    private Vector3Variable _dogLocation;
    private BoomerangPlayerMovement _player;

    public ISBoomDead(Vector3Variable boomLocation, Vector3Variable dogLocation, BoomerangPlayerMovement boomerangPlayer)
    {
        _boomLocation = boomLocation;
        _dogLocation = dogLocation;
        _player = boomerangPlayer;
    }

    public void OnStateEnter()
    {

        _player.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        _player.GetComponent<BoxCollider>().enabled = false;
    }

    public void OnStateExit()
    {
        _player.GetComponent<MeshRenderer>().enabled = true;
        _player.GetComponent<BoxCollider>().enabled = true;
    }

    public void OnStateTick()
    {
        _player.transform.position = _dogLocation.value;
        _boomLocation.value = _player.transform.position;
    }
}

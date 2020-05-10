using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISStandby : IState
{
    protected Vector3Variable _location;
    protected Transform _playerTransform;

    public ISStandby(Vector3Variable location, Transform playerTransform)
    {
        _location = location;
        _playerTransform = playerTransform;
    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
    }

    public void OnStateTick()
    {
        _location.value = _playerTransform.position;
    }
}

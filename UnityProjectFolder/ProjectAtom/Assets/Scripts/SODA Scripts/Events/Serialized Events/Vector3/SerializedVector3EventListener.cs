using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Vector3AssetUnityEvent : UnityEvent<Vector3> { }
public class SerializedVector3EventListener : SerializedParameterizedEventListener<Vector3>
{
    [SerializeField] SerializedVector3Event serializedVector3Event = null;
    [SerializeField] Vector3AssetUnityEvent vector3AssetUnityEvent = null;

    protected override void OnEnable()
    {
        serializedEvent = serializedVector3Event;
        response = vector3AssetUnityEvent;
        base.OnEnable();
    }

}

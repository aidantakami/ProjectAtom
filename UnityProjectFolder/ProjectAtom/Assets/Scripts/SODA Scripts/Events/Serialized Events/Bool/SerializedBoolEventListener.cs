using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class BoolAssetUnityEvent : UnityEvent<bool> { }
public class SerializedBoolEventListener : SerializedParameterizedEventListener<bool>
{
    [SerializeField] SerializedBoolEvent serializedBoolEvent = null;
    [SerializeField] BoolAssetUnityEvent boolAssetUnityEvent = null;

    protected override void OnEnable()
    {
        serializedEvent = serializedBoolEvent;
        response = boolAssetUnityEvent;
        base.OnEnable();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class IntAssetUnityEvent : UnityEvent<int> { }
public class SerializedIntEventListener : SerializedParameterizedEventListener<int>
{
    [SerializeField] SerializedIntEvent serializedIntEvent = null;
    [SerializeField] IntAssetUnityEvent intAssetUnityEvent = null;

    protected override void OnEnable()
    {
        serializedEvent = serializedIntEvent;
        response = intAssetUnityEvent;
        base.OnEnable();
    }

}

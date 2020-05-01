using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class FloatAssetUnityEvent : UnityEvent<float> { }
public class SerializedFloatEventListener : SerializedParameterizedEventListener<float>
{
    [SerializeField] SerializedFloatEvent serializedFloatEvent = null;
    [SerializeField] FloatAssetUnityEvent floatAssetUnityEvent = null;

    protected override void OnEnable()
    {
        serializedEvent = serializedFloatEvent;
        response = floatAssetUnityEvent;
        base.OnEnable();
    }

}

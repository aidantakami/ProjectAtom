using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class StringAssetUnityEvent : UnityEvent<string> { }
public class SerializedStringEventListener : SerializedParameterizedEventListener<string>
{
    [SerializeField] SerializedStringEvent serializedStringEvent = null;
    [SerializeField] StringAssetUnityEvent stringAssetUnityEvent = null;

    protected override void OnEnable()
    {
        serializedEvent = serializedStringEvent;
        response = stringAssetUnityEvent;
        base.OnEnable();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Vector2AssetUnityEvent : UnityEvent<Vector2> { }
public class SerializedVector2EventListener : SerializedParameterizedEventListener<Vector2>
{
    [SerializeField] SerializedVector2Event serializedVector2Event = null;
    [SerializeField] Vector2AssetUnityEvent vector2AssetUnityEvent = null;

    protected override void OnEnable()
    {
        serializedEvent = serializedVector2Event;
        response = vector2AssetUnityEvent;
        base.OnEnable();
    }

}

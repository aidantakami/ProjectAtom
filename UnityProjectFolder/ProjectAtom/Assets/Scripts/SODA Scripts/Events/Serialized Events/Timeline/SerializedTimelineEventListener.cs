using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Events;


[System.Serializable]
public class TimelineAssetUnityEvent : UnityEvent<TimelineAsset> { }
public class SerializedTimelineEventListener : SerializedParameterizedEventListener<TimelineAsset>
{
    [SerializeField] SerializedTimelineEvent serializedTimelineEvent = null;
    [SerializeField] TimelineAssetUnityEvent timelineAssetUnityEvent = null;

    protected override void OnEnable()
    {
        serializedEvent = serializedTimelineEvent;
        response = timelineAssetUnityEvent;
        base.OnEnable();
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "Timeline Event", menuName = "Sequencing/New Timeline Event")]
[System.Serializable]
public class SerializedTimelineEvent : SerializedParameterizedEvent<TimelineAsset>
{ }

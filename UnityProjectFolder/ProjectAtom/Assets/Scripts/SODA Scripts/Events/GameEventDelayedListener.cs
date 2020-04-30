using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDelayedListener : GameEventListener
{
    public float timeToWait = 1.0f;
    public bool scaledTime = true;

    public override void OnEventRaised()
    {
        StartCoroutine(DelayedEventRaise());
    }

    private IEnumerator DelayedEventRaise()
    {
        if (scaledTime)
        {
            yield return new WaitForSeconds(timeToWait);
        }
        else
        {
            yield return new WaitForSecondsRealtime(timeToWait);
        }

        base.OnEventRaised();

    }
}

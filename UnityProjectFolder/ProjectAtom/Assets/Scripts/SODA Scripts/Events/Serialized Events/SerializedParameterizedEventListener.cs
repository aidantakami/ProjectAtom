using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SerializedParameterizedEventListener<T> : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public SerializedParameterizedEvent<T> serializedEvent;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<T> response;

    [Tooltip("Priority at which this listener should activate. Default 0: higher numbers get activated earlier, lower numbers later.")]
    public int priority = 0;

    public bool runOnlyOnce = false;

    [Tooltip("Use a delay.")]
    public bool delayed = false;
    [Tooltip("How long to wait before firing.")]
    public float timeToWait = 1.0f;
    [Tooltip("Use scaled time for delay.")]
    public bool scaledTime = true;

    protected virtual void OnEnable()
    {
        serializedEvent.RegisterListener(this);
    }

    protected virtual void OnDisable()
    {
        serializedEvent.UnregisterListener(this);
    }

    public virtual void OnEventRaised(T parameter)
    {
        if (delayed)
        {
            StartCoroutine(DelayedEventRaise(parameter));
        }
        else
        {
            response.Invoke(parameter);
        }

        if (runOnlyOnce)
        {
            enabled = false;
        }
    }

    private IEnumerator DelayedEventRaise(T parameter)
    {
        if (scaledTime)
        {
            yield return new WaitForSeconds(timeToWait);
        }
        else
        {
            yield return new WaitForSecondsRealtime(timeToWait);
        }

        response.Invoke(parameter);
    }
}

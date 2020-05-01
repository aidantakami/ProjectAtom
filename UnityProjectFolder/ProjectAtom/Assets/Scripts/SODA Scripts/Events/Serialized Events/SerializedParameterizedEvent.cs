using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedParameterizedEvent<T> : ScriptableObject
{

    private readonly List<SerializedParameterizedEventListener<T>> eventListeners =
        new List<SerializedParameterizedEventListener<T>>();

    public void Raise(T parameter)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(parameter);
    }

    public void RegisterListener(SerializedParameterizedEventListener<T> listener)
    {
        if (!eventListeners.Contains(listener))
        {
            for (int i = 0; i < eventListeners.Count; i++)
            {
                if (eventListeners[i].priority < listener.priority)
                {
                    eventListeners.Insert(i, listener);
                    return;
                }
            }

            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(SerializedParameterizedEventListener<T> listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }

}

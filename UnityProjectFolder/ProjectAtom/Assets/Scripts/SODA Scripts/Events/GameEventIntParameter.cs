using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventIntParameter : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameEventIntListener> eventListeners =
        new List<GameEventIntListener>();

    public void Raise(int parameter)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(parameter);
    }

    public void RegisterListener(GameEventIntListener listener)
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

    public void UnregisterListener(GameEventIntListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}

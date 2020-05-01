using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    //List of listeners Event has registered

    private readonly List<GameEventListener> eventListeners = 
        new List<GameEventListener>();

    //Iterates through list backwards
    public void Raise()
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
        {
            for (int i = 0; i < eventListeners.Count; i++)
            {
                if(eventListeners[i].priority < listener.priority)
                {
                    eventListeners.Insert(i, listener);
                    return;
                }
            }

            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}

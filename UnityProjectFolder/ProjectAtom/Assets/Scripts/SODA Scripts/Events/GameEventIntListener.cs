using UnityEngine;
using UnityEngine.Events;


[System.Serializable] public class IntParameterUnityEvent : UnityEvent<int> { }
public class GameEventIntListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEventIntParameter Event;

    [Tooltip("Response to invoke when Event is raised.")]
    [SerializeField] public IntParameterUnityEvent Response;

    [Tooltip("Priority at which this listener should activate. Default 0: higher numbers get activated earlier, lower numbers later.")]
    public int priority = 0;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(int parameter)
    {
        Response.Invoke(parameter);
    }
}

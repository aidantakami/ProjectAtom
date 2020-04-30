using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    //List will store set
    public List<T> items = new List<T>();

    //Adds if not already contained
    public void Add(T thing)
    {
        if (!items.Contains(thing))
            items.Add(thing);
    }

    //Removes if contained
    public void Remove(T thing)
    {
        if (items.Contains(thing))
            items.Remove(thing);
    }

}
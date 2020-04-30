using System;
using UnityEngine;

[Serializable]
public class Vector3Reference
{
    public bool useConstant = true;
    public Vector3 constantValue;
    public Vector3Variable variable;

    public Vector3Reference()
    { }

    public Vector3Reference(Vector3 value)
    {
        useConstant = true;
        constantValue = value;
    }

    //if useConstant, return constantValue, else
    public Vector3 Value
    {
        get { return useConstant ? constantValue : variable.value; }
    }

    //implicit conversion
    public static implicit operator Vector3(Vector3Reference reference) => reference.Value;

    //Getters
    public float x { get { return Value.x; } }
    public float y { get { return Value.y; } }
    public float z { get { return Value.z; } }

    //Return square magnitude
    public float sqrMagnitude {
        get { return Value.sqrMagnitude; }
    }

}
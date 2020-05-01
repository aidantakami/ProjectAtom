using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StringVariable", menuName = "SODA Variables/StringVariable")]
public class StringVariable : SODAVariable
{
    public string value;

    public void SetValue(string v)
    {
        value = v;
    }

    public void SetValue(StringVariable v)
    {
        value = v.value;
    }

    public void ModValue(string amount)
    {
        value += amount;
    }

    public void ModValue(FloatVariable amount)
    {
        value += amount.value;
    }

    public static implicit operator string(StringVariable reference)
    {
        return reference.value;
    }

    public override string ToString()
    {
        return value;
    }
}

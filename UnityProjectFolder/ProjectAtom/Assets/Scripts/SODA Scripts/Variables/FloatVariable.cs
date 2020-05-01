using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "SODA Variables/FloatVariable")]
public class FloatVariable : SODAVariable
{
    public float value;

    public void SetValue(float v)
    {
        value = v;
    }

    public void SetValue(FloatVariable v)
    {
        value = v.value;
    }

    public void ModValue(float amount)
    {
        value += amount;
    }

    public void ModValue(FloatVariable amount)
    {
        value += amount.value;
    }

    public static implicit operator float(FloatVariable reference)
    {
        return reference.value;
    }

    public override string ToString() {
        return "" + value;
    }

    //Operator commands for 2 FloatVariables
    public static bool operator >(FloatVariable a, FloatVariable b) => (a.value > b.value);
    public static bool operator <(FloatVariable a, FloatVariable b) => (a.value < b.value);
    public static bool operator >=(FloatVariable a, FloatVariable b) => (a.value >= b.value);
    public static bool operator <=(FloatVariable a, FloatVariable b) => (a.value <= b.value);


    //Operator commands for float variable and float
    public static bool operator >(FloatVariable a, float b) => (a.value > b);
    public static bool operator <(FloatVariable a, float b) => (a.value < b);
    public static bool operator >=(FloatVariable a, float b) => (a.value >= b);
    public static bool operator <=(FloatVariable a, float b) => (a.value <= b);
}
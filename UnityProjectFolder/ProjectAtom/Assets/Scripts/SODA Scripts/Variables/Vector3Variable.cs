using UnityEngine;

[CreateAssetMenu(fileName = "Vector3Variable", menuName = "SODA Variables/Vector3Variable")]
public class Vector3Variable : SODAVariable
{
    //Value stored by Vector3Variable
    public Vector3 value;

    //Stores values in Vector3Variable
    public float X { get { return value.x; } }
    public float Y { get { return value.y; } }
    public float Z { get { return value.z; } }

    //Returns square magnitude of Vector3Variable
    public float SqrMagnitude {
        get { return Mathf.Pow(this.X, 2) + Mathf.Pow(this.Y,2) + Mathf.Pow(this.Z, 2); }
    }

    //Setter which takes V3
    public void SetValue(Vector3 v)
    {
        value = v;
    }

    //Setter which takes V3V
    public void SetValue(Vector3Variable v)
    {
        value = v.value;
    }


    public void ModValue(Vector3 amount)
    {
        value += amount;
    }

    public void ModValue(Vector3Variable amount)
    {
        value += amount.value;
    }

    //ToString Override
    public override string ToString() {
        return "(" + value.x + ", " + value.y + ", " + value.z + ")";
    }

}
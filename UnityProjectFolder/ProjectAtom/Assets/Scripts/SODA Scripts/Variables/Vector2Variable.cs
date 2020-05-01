using UnityEngine;

[CreateAssetMenu(fileName = "Vector2Variable", menuName = "SODA Variables/Vector2Variable")]
public class Vector2Variable : SODAVariable
{

    public Vector2 value;

    //Gets values from value
    public float X { get { return value.x; } }
    public float Y { get { return value.y; } }


    //Return square magnitude
    public float SqrMagnitude {
        get { return Mathf.Pow(X, 2) + Mathf.Pow(Y,2); }
    }

    //Setter which takes V2
    public void SetValue(Vector2 v)
    {
        value = v;
    }

    //Setter which takes V2V
    public void SetValue(Vector2Variable v)
    {
        value = v.value;
    }

    //Mod value
    public void ModValue(Vector2 amount)
    {
        value += amount;
    }

    public void ModValue(Vector2Variable amount)
    {
        value += amount.value;
    }


    //ToString Override
    public override string ToString() {
        return "(" + value.x + "," + value.y + ")";
    }

}
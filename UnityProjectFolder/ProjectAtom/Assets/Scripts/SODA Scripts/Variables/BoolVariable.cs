using UnityEngine;

[CreateAssetMenu(fileName = "BoolVariable", menuName = "SODA Variables/BoolVariable")]
public class BoolVariable : SODAVariable
{

    public bool value;
    
    public void SetValue(bool v)
    {
        value = v;
    }

    public void SetValue(BoolVariable v)
    {
        value = v.value;
    }

    public override string ToString() {
        return "" + value;
    }


}
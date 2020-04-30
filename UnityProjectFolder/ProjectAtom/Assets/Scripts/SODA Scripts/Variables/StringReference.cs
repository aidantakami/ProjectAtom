using System;

[Serializable]
public class StringReference
{
    public bool useConstant = true;
    public string constantValue;
    public StringVariable variable;

    public StringReference()
    { }

    public StringReference(string value)
    {
        useConstant = true;
        constantValue = value;
    }

    public string Value
    {
        get { return useConstant ? constantValue : variable.value; }
    }

    public static implicit operator string(StringReference reference)
    {
        return reference.Value;
    }
}
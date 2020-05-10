using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    [SerializeField] public BoolVariable isPaused;
    [SerializeField] public BoolVariable boomThrown;

    public void Awake()
    {
        isPaused.SetValue(false);
        boomThrown.SetValue(false);
    }
}

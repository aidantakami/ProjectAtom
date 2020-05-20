using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    [SerializeField] public BoolVariable isPaused;
    [SerializeField] public BoolVariable boomThrown;
    [SerializeField] public BoolVariable canPlayerMove;
    [SerializeField] public FloatVariable boomerangLifeRemaining;
    [SerializeField] public FloatVariable springboardCooldownRemaining;

    public void Awake()
    {
        isPaused.SetValue(false);
        boomThrown.SetValue(false);
        canPlayerMove.SetValue(true);

        boomerangLifeRemaining.SetValue(0);
        springboardCooldownRemaining.SetValue(0);
    }

    public void VariableManagerRestart()
    {
        isPaused.SetValue(false);
        boomThrown.SetValue(false);
        canPlayerMove.SetValue(true);

        boomerangLifeRemaining.SetValue(0);
        springboardCooldownRemaining.SetValue(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    //Stores current and previous state
    public IState currentState;
    private IState prevState;

    //Enters new state and starts OnStateEnter
    public void EnterState(IState newState)
    {
        if(currentState!= null)
        {
            prevState = currentState;
        }


        if (newState != currentState)
        {
            currentState = newState;
            currentState.OnStateEnter();
        }
    }

    //Ticks the current state
    public void Tick() 
    {
        if(currentState != null)
        {
            currentState.OnStateTick();
        }

    }

    //Returns to previous state
    public void ReturnToPreviousState()
    {
        IState temp = prevState;
        prevState = currentState;
        currentState = temp;
    }
}

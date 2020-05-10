using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] public BoolVariable isPaused;
    [SerializeField] public BoolVariable isBoomThrown;

    [SerializeField] public UnityEvent GameBegin = new UnityEvent();
    [SerializeField] public UnityEvent PauseGameEvent = new UnityEvent();
    [SerializeField] public UnityEvent UnpauseGameEvent = new UnityEvent();
    [SerializeField] public UnityEvent RestartGameEvent = new UnityEvent();

    private bool playerCanPause = false;
    private int totalPlayersIn = 0;

    // Update reads for player's pause
    void Update()
    {

        //If the player can pause
        if (playerCanPause)
        {
            //if not already paused
            if (!isPaused.value)
            {
                //read for players pauses
                if (Input.GetButtonDown("P1Start") || Input.GetButtonDown("P2Start"))
                {
                    PauseGame();
                    return;
                }
            }
            //Else if already paused
            else if (isPaused.value)
            {
                Debug.Log("Unpaused");
                //Unpause
                if (Input.GetButtonDown("P1Start") || Input.GetButtonDown("P2Start"))
                {
                    UnpauseGame();
                    return;
                }
            }
        }      
    }
    //Event Responses
#region
    public void BoomerangThrown()
    {
        isBoomThrown.SetValue(true);
    }

    public void BoomerangReturned()
    {
        isBoomThrown.SetValue(false);
    }
#endregion


    //Functions
#region
    //Pauses Game for players
    public void PauseGame()
    {
        isPaused.SetValue(true);
        PauseGameEvent.Invoke();
    }

    public void UnpauseGame()
    {
        isPaused.SetValue(false);
        UnpauseGameEvent.Invoke();
    }

    public void ExternalUnpause()
    {
        isPaused.SetValue(false);
    }

    public void RestartGame()
    {
        RestartGameEvent.Invoke();
    }

    
    //Keeps track as players sign in
    public void PlayerSignedIn(int playerNumber)
    {
        //Tracks number of players signed in
        totalPlayersIn++;

        //If all players are in
        if(totalPlayersIn == 2)
        {
            //Begin game
            GameBegin.Invoke();
            playerCanPause = true;
        }
    }

#endregion
}

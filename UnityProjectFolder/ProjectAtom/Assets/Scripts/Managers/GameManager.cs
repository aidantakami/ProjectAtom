using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] public BoolVariable isPaused;
    [SerializeField] public BoolVariable isBoomThrown;

    [System.Serializable]
    public class PlayerSignInEventType : UnityEvent<int>
    {
    }

    [SerializeField] PlayerSignInEventType playerSignInEvent = new PlayerSignInEventType();
    [SerializeField] public UnityEvent GameBegin = new UnityEvent();
    [SerializeField] public UnityEvent PauseGameEvent = new UnityEvent();
    [SerializeField] public UnityEvent UnpauseGameEvent = new UnityEvent();
    [SerializeField] public UnityEvent RestartGameEvent = new UnityEvent();
    [SerializeField] public UnityEvent EndGameEvent = new UnityEvent();
    [SerializeField] public UnityEvent BoomerangDeadEvent = new UnityEvent();

    private bool playerCanPause = false;
    private bool gameIsEnded = true;
    private bool p1SignedIn = false;
    private bool p2SignedIn = false;
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
                if (Input.GetButtonDown("P1Start") || Input.GetButtonDown("P2Start") || Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGame();
                    return;
                }
            }
            //Else if already paused
            else if (isPaused.value)
            {
                //Unpause
                if (Input.GetButtonDown("P1Start") || Input.GetButtonDown("P2Start") || Input.GetKeyDown(KeyCode.Escape))
                {
                    UnpauseGame();
                    return;
                }
            }
        }

        if (gameIsEnded)
        {
            if((Input.GetButtonDown("P1A Button") || Input.GetKeyDown(KeyCode.E)) && !p1SignedIn)
            {
                PlayerSignedIn(1);
                p1SignedIn = true;
            }
            
            if((Input.GetButtonDown("P2B Button") || Input.GetKeyDown(KeyCode.RightShift))&& !p2SignedIn)
            {
                PlayerSignedIn(2);
                p2SignedIn = true;
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
        playerSignInEvent.Invoke(playerNumber);

        //Tracks number of players signed in
        totalPlayersIn++;

        //If all players are in
        if(totalPlayersIn == 2)
        {
            //Begin game
            GameBegin.Invoke();
            playerCanPause = true;
            totalPlayersIn = 0;
        }
    }

    //End of game event invoked
    public void EndGame()
    {
        EndGameEvent.Invoke();
        gameIsEnded = true;
        p1SignedIn = false;
        p2SignedIn = false;
    }

    public void EndGameExternalEvent()
    {
        gameIsEnded = true;
        p1SignedIn = false;
        p2SignedIn = false;
    }

    public void BoomerangDead()
    {
        BoomerangDeadEvent.Invoke();
    }

    #endregion
}

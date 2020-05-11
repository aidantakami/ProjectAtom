using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XInputDotNetPure;

public class ControllerManager : MonoBehaviour
{
    private PlayerIndex controllerNumber1;
    private PlayerIndex controllerNumber2;

    GamePadState p1State;
    GamePadState p2State;

    private bool player1SignedIn = false;
    private bool player2SignedIn = false;

    [System.Serializable]
    public class PlayerSignedIn : UnityEvent<int>
    {
    }

    [SerializeField]
    PlayerSignedIn playerSignInEvent = new PlayerSignedIn();

    // Start is called before the first frame update
    void Awake()
    {
        controllerNumber1 = PlayerIndex.One;
        controllerNumber2 = PlayerIndex.Two;
    }


    public void Start()
    {
        StartCoroutine(SignInControllers());
    }


    //IEnumerator which will allow players to see what player number they are
    public IEnumerator SignInControllers()
    {

        while(!player1SignedIn || !player2SignedIn)
        {
            GamePadState p1SignIn = GamePad.GetState(controllerNumber1);
            GamePadState p2SignIn = GamePad.GetState(controllerNumber2);

            if (Input.GetButtonDown("P1A Button") && !player1SignedIn)
            {
                StartCoroutine(ShortControllerVibration(controllerNumber1));
                playerSignInEvent.Invoke(1);
                player1SignedIn = true;
            }

            if(Input.GetButtonDown("P2B Button") && !player2SignedIn)
            {
                StartCoroutine(ShortControllerVibration(controllerNumber2));
                playerSignInEvent.Invoke(2);
                player2SignedIn = true;
            }
            yield return new WaitForSeconds(0.001f);
        }

    }

    //Short Controller Vibration 
    public void StartShortVibration(int controllerNumber)
    {
        if(controllerNumber == 1)
        {
            StartCoroutine(ShortControllerVibration(controllerNumber1));
        }
        else if(controllerNumber == 2)
        {
            StartCoroutine(ShortControllerVibration(controllerNumber2));
        }
    }

    private IEnumerator ShortControllerVibration(PlayerIndex controller)
    {
       GamePad.SetVibration(controller, 100f, 100f);

       yield return new WaitForSeconds(0.5f);

       GamePad.SetVibration(controller, 0f, 0f);
    }




    //Medium Controller Vibration
    public void StartMediumVibration(int controllerNumber)
    {
        if (controllerNumber == 1)
        {
            StartCoroutine(MediumControllerVibration(controllerNumber1));
        }
        else if (controllerNumber == 2)
        {
            StartCoroutine(MediumControllerVibration(controllerNumber2));
        }
    }

    private IEnumerator MediumControllerVibration(PlayerIndex controller)
    {
        GamePad.SetVibration(controller, 100f, 100f);

        yield return new WaitForSeconds(1);

        GamePad.SetVibration(controller, 0f, 0f);
    }




    //Un Timed manual vibration
    public void StartControllerVibration(int controllerNumber)
    {
        if(controllerNumber == 1)
        {
            GamePad.SetVibration(controllerNumber1, 100f, 100f);
        }
        else if(controllerNumber == 2)
        {
            GamePad.SetVibration(controllerNumber2, 100f, 100f);
        }
        
    }

    public void EndControllerVibration(int controllerNumber)
    {
        if (controllerNumber == 1)
        {
            GamePad.SetVibration(controllerNumber1, 0f, 0f);
        }
        else if (controllerNumber == 2)
        {
            GamePad.SetVibration(controllerNumber2, 0f, 0f);
        }
    }



    //Timed Controller Vibration
    public void StartTimedVibration(int controllerNumber, float seconds)
    {
        if(controllerNumber == 1)
        {
            StartCoroutine(TimedControllerVibration(controllerNumber1, seconds));
        }
        else if(controllerNumber == 2)
        {
            StartCoroutine(TimedControllerVibration(controllerNumber2, seconds));
        }
    }

    private IEnumerator TimedControllerVibration(PlayerIndex controller, float seconds)
    {

        GamePad.SetVibration(controller, 100f, 100f);

        yield return new WaitForSeconds(seconds);

        GamePad.SetVibration(controller, 0f, 0f);
    }

    public void BoomerangCaughtVibration()
    {
        StartTimedVibration(1, 0.1f);
        StartTimedVibration(2, 0.1f);
    }
}

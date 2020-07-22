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

    // Start is called before the first frame update
    void Awake ()
    {
        controllerNumber1 = PlayerIndex.One;
        controllerNumber2 = PlayerIndex.Two;
    }

    //Short Controller Vibration 
    public void StartShortVibration (int controllerNumber)
    {

        if (controllerNumber == 1)
        {
            StartCoroutine (ShortControllerVibration (controllerNumber1));
        }
        else if (controllerNumber == 2)
        {
            StartCoroutine (ShortControllerVibration (controllerNumber2));
        }
    }

    private IEnumerator ShortControllerVibration (PlayerIndex controller)
    {
        GamePad.SetVibration (controller, 100f, 100f);

        yield return new WaitForSeconds (0.5f);

        GamePad.SetVibration (controller, 0f, 0f);
    }

    //Medium Controller Vibration
    public void StartMediumVibration (int controllerNumber)
    {
        if (controllerNumber == 1)
        {
            StartCoroutine (MediumControllerVibration (controllerNumber1));
        }
        else if (controllerNumber == 2)
        {
            StartCoroutine (MediumControllerVibration (controllerNumber2));
        }
    }

    private IEnumerator MediumControllerVibration (PlayerIndex controller)
    {
        GamePad.SetVibration (controller, 100f, 100f);

        yield return new WaitForSeconds (1);

        GamePad.SetVibration (controller, 0f, 0f);
    }

    //Un Timed manual vibration
    public void StartControllerVibration (int controllerNumber)
    {
        if (controllerNumber == 1)
        {
            GamePad.SetVibration (controllerNumber1, 100f, 100f);
        }
        else if (controllerNumber == 2)
        {
            GamePad.SetVibration (controllerNumber2, 100f, 100f);
        }

    }

    public void EndControllerVibration (int controllerNumber)
    {
        if (controllerNumber == 1)
        {
            GamePad.SetVibration (controllerNumber1, 0f, 0f);
        }
        else if (controllerNumber == 2)
        {
            GamePad.SetVibration (controllerNumber2, 0f, 0f);
        }
    }

    //Timed Controller Vibration
    public void StartTimedVibration (int controllerNumber, float seconds)
    {
        if (controllerNumber == 1)
        {
            StartCoroutine (TimedControllerVibration (controllerNumber1, seconds));
        }
        else if (controllerNumber == 2)
        {
            StartCoroutine (TimedControllerVibration (controllerNumber2, seconds));
        }
    }

    private IEnumerator TimedControllerVibration (PlayerIndex controller, float seconds)
    {

        GamePad.SetVibration (controller, 100f, 100f);

        yield return new WaitForSeconds (seconds);

        GamePad.SetVibration (controller, 0f, 0f);
    }

    public void BoomerangCaughtVibration ()
    {
        StartTimedVibration (1, 0.1f);
        StartTimedVibration (2, 0.1f);
    }

    public void GameOverVibration ()
    {
        StartTimedVibration (1, 0.5f);
        StartTimedVibration (2, 0.5f);
    }

    //Will proc if dog player collides with ability token
    public void AbilityTokenVibration (bool dogPlayer)
    {
        if (dogPlayer)
        {
            StartTimedVibration (1, 0.1f);
        }
        else StartTimedVibration (2, 0.1f);
    }

    public void DogAttackVibration ()
    {
        StartTimedVibration (1, 1f);
    }

    public void BoomReviveVibration ()
    {
        StartTimedVibration (1, 0.4f);
        StartTimedVibration (2, 0.7f);
    }

    public void GustOfWindVibration ()
    {
        StartTimedVibration (2, 0.3f);
    }

    public void EndAllVibrations ()
    {
        GamePad.SetVibration (controllerNumber1, 0f, 0f);
        GamePad.SetVibration (controllerNumber2, 0f, 0f);

    }
}
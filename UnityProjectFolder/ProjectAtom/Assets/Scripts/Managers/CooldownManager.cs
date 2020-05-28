using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CooldownManager : MonoBehaviour
{
    [SerializeField] public FloatVariable boomerangLifeRemaining;
    [SerializeField] public float boomerangTotalLife;

    [SerializeField] public UnityEvent boomDeadEvent = new UnityEvent ();

    private bool boomerangOut = false;
    private bool gameIsPaused = false;

    // Update is called once per frame
    void Update ()
    {
        //Will tick down time remaining
        if (boomerangOut)
        {
            if (!gameIsPaused)
            {
                boomerangLifeRemaining.value -= Time.deltaTime;

                if (boomerangLifeRemaining.value < 0)
                {
                    //Boomerang Die Event
                    boomDeadEvent.Invoke ();
                    boomerangOut = false;
                }
            }
        }
    }

    public void BoomerangThrown ()
    {
        boomerangOut = true;
        boomerangLifeRemaining.SetValue (boomerangTotalLife);
    }

    public void BoomerangCaught ()
    {
        boomerangOut = false;
    }

    public void CooldownManagerPause ()
    {
        gameIsPaused = true;
    }

    public void CooldownManagerUnpause ()
    {
        gameIsPaused = false;
    }

    public void CooldownManagerRestart ()
    {

        gameIsPaused = false;
        boomerangOut = false;
        boomerangLifeRemaining.SetValue (boomerangTotalLife);

    }

    public void BoomerangDeadByOtherCauses ()
    {
        boomerangOut = false;
        boomerangLifeRemaining.SetValue (boomerangTotalLife);
    }
}
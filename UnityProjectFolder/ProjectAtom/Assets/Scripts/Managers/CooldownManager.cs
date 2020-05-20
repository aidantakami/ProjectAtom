﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CooldownManager : MonoBehaviour
{
    [SerializeField] public FloatVariable boomerangLifeRemaining;
    [SerializeField] public float boomerangTotalLife;

    [SerializeField] public FloatVariable springboardCooldownRemaining;
    [SerializeField] public float springboardTotalCooldown;

    [SerializeField] public UnityEvent boomDeadEvent = new UnityEvent();
    [SerializeField] public UnityEvent springboardExpiredEvent = new UnityEvent();


    private bool boomerangOut = false;
    private bool springboardOut = false;

    // Update is called once per frame
    void Update()
    {
        //Will tick down time remaining
        if (boomerangOut)
        {
            boomerangLifeRemaining.value -= Time.deltaTime;

            if(boomerangLifeRemaining.value < 0)
            {
                //Boomerang Die Event
                boomDeadEvent.Invoke();
                boomerangOut = false;
            }
        }

        //If springboard has been placed
        if (springboardOut)
        {
            springboardCooldownRemaining.value -= Time.deltaTime;

            if(springboardCooldownRemaining.value < 0)
            {
                springboardExpiredEvent.Invoke();
                springboardOut = false;
            }
        }
    }


    public void BoomerangThrown()
    {
        boomerangOut = true;
        boomerangLifeRemaining.SetValue(boomerangTotalLife);
    }

    public void BoomerangCaught()
    {
        boomerangOut = false;
    }

    public void SpringboardPlaced()
    {
        springboardOut = true;
        springboardCooldownRemaining.SetValue(springboardTotalCooldown);
    }
}
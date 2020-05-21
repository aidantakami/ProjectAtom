﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class AbilityTokenScript : MonoBehaviour
{

    private float amplitude = 0.4f;
    private float frequency;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    Vector3 tempVector = new Vector3();


    [System.Serializable]
    public class AbilityTokenEvent : UnityEvent<bool>
    {
    }

    [SerializeField] public AbilityTokenEvent dogPickedUpToken = new AbilityTokenEvent();


    public void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;

        frequency = Random.Range(0.3f, 0.9f);

    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        tempVector.Set(transform.position.x, tempPos.y, transform.position.z);

        transform.position = tempVector;
    }

        public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DogPlayer"))
        {
            gameObject.SetActive(false);

            //Give ability token to dog player
            dogPickedUpToken.Invoke(true);
        }
        else if (other.gameObject.CompareTag("BoomerangPlayer"))
        {
            gameObject.SetActive(false);

            //Give ability token to boomerang player
            dogPickedUpToken.Invoke(false);
        }
    }
}
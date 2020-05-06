using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Triggers
        float rTriggerFloat = Input.GetAxis("Right Trigger");
        float lTriggerFloat = Input.GetAxis("Left Trigger");

        //Buttons
        bool leftBumper = Input.GetButton("Left Bumper");
        bool rightBumper = Input.GetButton("Right Bumper");
        bool backButton = Input.GetButton("Back");
        bool startButton = Input.GetButton("Start");
        bool aButton = Input.GetButton("A Button");
        bool bButton = Input.GetButton("B Button");
        bool xButton = Input.GetButton("X Button");
        bool yButton = Input.GetButton("Y Button");

        //DPad
        float dpadHorizontal = Input.GetAxis("Dpad Horizontal");
        float dpadVertical = Input.GetAxis("Dpad Vertical");

        //JoySticks
        float lHorizontal = Input.GetAxis("Horizontal");
        float rHorizontal = Input.GetAxis("P1Right Stick Horizontal");
        float lVertical = Input.GetAxis("Vertical");
        float rVertical = Input.GetAxis("P1Right Stick Vertical");

        float p2rHorizontal = Input.GetAxis("P2Right Stick Horizontal");
        float p2rVertical = Input.GetAxis("P2Right Stick Vertical");


        if (aButton)
        {
            Debug.Log("A button Pressed");
        }

        if (bButton)
        {
            Debug.Log("B button Pressed");
        }
        if (xButton)
        {
            Debug.Log("X button Pressed");
        }

        if (yButton)
        {
            Debug.Log("Y button Pressed");
        }

        if (leftBumper)
        {
            Debug.Log("Left Bumper Pressed");
        }

        if (rightBumper)
        {
            Debug.Log("Right Bumper Pressed");
        }

        if(lTriggerFloat > 0)
        {
            Debug.Log("Left Trigger: " + lTriggerFloat);
        }

        if (rTriggerFloat > 0)
        {
            Debug.Log("Right Trigger: " + rTriggerFloat);
        }

        if (lVertical > 0)
        {
            Debug.Log("Left Vertical: " + lVertical);
        }

        if (rVertical > 0)
        {
            Debug.Log("P1Right Vertical: " + rVertical);
        }

        if (lHorizontal > 0)
        {
            Debug.Log("Left Horizontal: " + lHorizontal);
        }

        if (rHorizontal > 0)
        {
            Debug.Log("P1Right Horizontal: " + rHorizontal);
        }


        if(p2rHorizontal > 0)
        {
            Debug.Log("P2Right Horizontal: " + p2rHorizontal);
        }

        if(p2rVertical > 0)
        {
            Debug.Log("P2Right Vertical: " + p2rVertical);
        }




        if (dpadHorizontal > 0)
        {
            Debug.Log("DPad Horizontal: " + dpadHorizontal);
        }

        if (dpadVertical > 0)
        {
            Debug.Log("DPad Vertical: " + dpadVertical);
        }
    }
}

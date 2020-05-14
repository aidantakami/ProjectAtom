using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //float p1RS = Input.GetAxis("P1Right Stick Horizontal");
        float p2RT = Input.GetAxis("P2Right Trigger");
        float p1LT = Input.GetAxis("P1Left Trigger");
        float p2LT = Input.GetAxis("P2Left Trigger");

        //if (p1RS != 0) Debug.Log(p1RS.ToString());
        if (p2RT > 0) Debug.Log(p2RT.ToString());
        if (p1LT > 0) Debug.Log(p1LT.ToString());
        if (p2LT > 0) Debug.Log(p2LT.ToString());

    }
}

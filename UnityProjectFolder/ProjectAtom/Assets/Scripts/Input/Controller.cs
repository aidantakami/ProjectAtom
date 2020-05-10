using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float p1RT = Input.GetAxis("P1Right Trigger");
        float p2RT = Input.GetAxis("P2Right Trigger");
        float p1LT = Input.GetAxis("P1Left Trigger");
        float p2LT = Input.GetAxis("P2Left Trigger");

        if (p1RT > 0) Debug.Log(p1RT.ToString());
        if (p2RT > 0) Debug.Log(p2RT.ToString());
        if (p1LT > 0) Debug.Log(p1LT.ToString());
        if (p2LT > 0) Debug.Log(p2LT.ToString());

    }
}

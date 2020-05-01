using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Parameterized UnityEvent must be called as a class
[System.Serializable]
public class BoolEvent : UnityEvent<Vector3> { }


//For testing only
public class EventTestScript : MonoBehaviour
{

    public BoolEvent newUnityEvent;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("true sent");
            newUnityEvent.Invoke(new Vector3 (1, 1, 1));
        }
        else if (Input.GetKeyDown(KeyCode.W)) 
        {
            newUnityEvent.Invoke(new Vector3 (2,2, 2));
        }
    }
}

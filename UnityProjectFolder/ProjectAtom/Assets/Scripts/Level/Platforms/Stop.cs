using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    [SerializeField] float timeToWait;
    [SerializeField] FloatVariable speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        float temp = speed;
        Debug.Log(temp);
        speed.SetValue(0);
        StartCoroutine(WaitAndRelease(timeToWait, temp));
        Debug.Log("hit");
    }

    IEnumerator WaitAndRelease(float time, float originalSpeed)
    {
        yield return new WaitForSeconds(time);
        speed.SetValue(originalSpeed);
    }
}

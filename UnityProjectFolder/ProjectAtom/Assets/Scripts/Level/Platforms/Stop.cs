using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    [SerializeField] float timeToWait;
    [SerializeField] FloatVariable speed;
    [SerializeField] BoolVariable canMove;
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
        canMove.SetValue(false);
        Debug.Log(temp);
        StartCoroutine(WaitAndRelease(timeToWait, temp));
        gameObject.GetComponent<Collider>().enabled = false;
    }

    IEnumerator WaitAndRelease(float time, float originalSpeed)
    {
        speed.SetValue(0);
        yield return new WaitForSeconds(time);
        speed.SetValue(originalSpeed);
        canMove.SetValue(true);
    }
}

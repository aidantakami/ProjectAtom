using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] float time, hitSpeed;
    float startTime, t, originalSpeed;
    [SerializeField] FloatVariable speed;
    bool hit;
    // Start is called before the first frame update
    void Start()
    {
        hit = false;
        startTime = Time.time;
        t = 0;
        originalSpeed = speed.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit == true)
        {
            ChangeSpeed();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        hit = true;
    }

    void ChangeSpeed()
    {
        t += Time.deltaTime / time;
        speed.SetValue(Mathf.Lerp(hitSpeed, originalSpeed, t));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpawner : MonoBehaviour
{

    [SerializeField] public GameObject boneToSpawn;
    public float spawnSpeed;
    public float rotateSpeedFloat;
    private GameObject boneRef;
    private float timeTemp = 0;
    private Vector3 fallSpeed = new Vector3 (0, -12, 0);
    private Vector3 rotateSpeed;

    // Start is called before the first frame update
    void Start ()
    {
        boneRef = boneToSpawn;
        boneRef.transform.position = transform.position;
        rotateSpeed = new Vector3 (0, rotateSpeedFloat, 0);
        boneRef.AddComponent (typeof (Rigidbody));
    }

    // Update is called once per frame
    void Update ()
    {
        timeTemp += Time.deltaTime;
        boneRef.GetComponent<Rigidbody> ().velocity = fallSpeed;

        if (timeTemp > spawnSpeed)
        {
            boneRef.transform.position = transform.position;
            timeTemp = 0;
        }
        boneRef.transform.Rotate (rotateSpeed, Space.Self);

    }

}
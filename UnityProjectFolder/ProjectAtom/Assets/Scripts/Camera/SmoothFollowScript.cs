using UnityEngine;
using System.Collections;

public class SmoothFollowScript : MonoBehaviour
{

    // The target we are following
    [SerializeField] public Vector3Variable target;

    // The distance in the x-z plane to the target
    public float minDistance = 10f;
    public float maxDistance = 15f;

    // the height we want the camera to be above the target
    public float height = 10f;

    // How much we want to Dampen height change
    public float heightDamping = 2.0f;

    private Vector3 startingPos;

    private void Awake()
    {
        startingPos = transform.position;
    }


    void LateUpdate()
    {
        // Early out if we don't have a target
        if (target != null)
        {
            // Calculate the current height angles
            float wantedHeight = target.value.y + height;

            float currentHeight = transform.position.y;

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);


            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target & height
            transform.position = new Vector3(target.value.x, currentHeight, target.value.z);

            if (Vector3.Distance(target.value, transform.position) < minDistance)
            {
                transform.position -= Vector3.forward * minDistance;
            }
            else if(Vector3.Distance(target.value, transform.position) > maxDistance)
            {
                transform.position += Vector3.forward * maxDistance;
            }



            // Always look at the target
            transform.LookAt(target.value);
        }
    }

    public void CameraFollowRestart()
    {
        Debug.Log("Restart received");
        transform.position = startingPos;
    }
}

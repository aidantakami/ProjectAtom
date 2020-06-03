using System.Collections;
using UnityEngine;

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

    public float heightForBoom;

    private Vector3 startingPos;
    private float boomerangStatusVar = 0;

    private void Awake ()
    {
        startingPos = transform.position;
    }

    void LateUpdate ()
    {
        // Early out if we don't have a target
        if (target != null)
        {
            // Calculate the current height angles
            float wantedHeight = target.value.y + height + boomerangStatusVar;

            float currentHeight = transform.position.y;

            // Damp the height
            currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target & height
            transform.position = new Vector3 (target.value.x, currentHeight, target.value.z);

            if (Vector3.Distance (target.value, transform.position) < minDistance)
            {
                transform.position -= Vector3.forward * minDistance;
            }
            else if (Vector3.Distance (target.value, transform.position) > maxDistance)
            {
                transform.position += Vector3.forward * maxDistance;
            }

            // Always look at the target
            transform.LookAt (target.value);
        }
    }

    public void CameraFollowRestart ()
    {
        BoomerangThrownEndCamera ();
        transform.position = startingPos;
    }

    public void BoomerangThrownStartCamera ()
    {
        StartCoroutine (BoomerangThrownCameraEvent ());
    }
    private IEnumerator BoomerangThrownCameraEvent ()
    {
        float rep = 0;
        while (rep < 1)
        {
            boomerangStatusVar = Mathf.Lerp (boomerangStatusVar, heightForBoom, rep);
            rep += 0.01f;
            yield return new WaitForEndOfFrame ();
        }

    }

    public void BoomerangThrownEndCamera ()
    {
        StartCoroutine (BoomerangBackEvent ());
    }
    private IEnumerator BoomerangBackEvent ()
    {
        float rep = 0;
        while (rep < 1)
        {
            boomerangStatusVar = Mathf.Lerp (boomerangStatusVar, 0, rep);
            rep += 0.01f;
            yield return new WaitForEndOfFrame ();
        }
    }
}
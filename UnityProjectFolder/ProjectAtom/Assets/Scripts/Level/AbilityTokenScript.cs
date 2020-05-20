using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class AbilityTokenScript : MonoBehaviour
{

    [System.Serializable]
    public class AbilityTokenEvent : UnityEvent<bool>
    {
    }

    [SerializeField] public AbilityTokenEvent dogPickedUpToken = new AbilityTokenEvent();

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

        public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DogPlayer"))
        {
            gameObject.SetActive(false);

            //Give ability token to dog player
            dogPickedUpToken.Invoke(true);
        }
        else if (other.gameObject.CompareTag("BoomerangPlayer"))
        {
            gameObject.SetActive(false);

            //Give ability token to boomerang player
            dogPickedUpToken.Invoke(false);
        }
    }
}

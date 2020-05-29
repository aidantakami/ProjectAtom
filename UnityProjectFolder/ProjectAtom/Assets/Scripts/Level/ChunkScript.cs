using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChunkScript : MonoBehaviour
{

    [SerializeField] public ChunkManager chunkManager;
    [SerializeField] public List<GameObject> itemsOnChunk = new List<GameObject> ();
    [SerializeField] public UnityEvent chunkCompleted = new UnityEvent ();

    // Start is called before the first frame update
    void Start ()
    {
        AddSelfToList ();

    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("DogPlayer"))
        {
            chunkCompleted.Invoke ();
        }
    }

    public void AddSelfToList ()
    {
        //Adds self to RTS
        chunkManager.Add (gameObject);
    }

    public void SetAllActive ()
    {

        foreach (GameObject element in itemsOnChunk)
        {
            if (element != null && !element.activeInHierarchy)
            {
                element.SetActive (true);
            }
        }
    }
}
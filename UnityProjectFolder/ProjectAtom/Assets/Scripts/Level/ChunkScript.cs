using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChunkScript : MonoBehaviour
{

    [SerializeField] public ChunkManager chunkManager;
    [SerializeField] public UnityEvent chunkCompleted = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        AddSelfToList();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chunkCompleted.Invoke();
        }
    }

    public void AddSelfToList()
    {
        //Adds self to RTS
        chunkManager.Add(gameObject);
    }
}

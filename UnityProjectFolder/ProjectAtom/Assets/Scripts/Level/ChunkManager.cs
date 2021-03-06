﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    //List of chunks
    [SerializeField] private List<GameObject> Chunks;

    private int playerOnChunk = 1;
    private int chunkIterator = 0;
    private Vector3 gapBetweenChunks;

    public GameObject firstChunk;
    private Vector3 startingPos;

    private bool firstChunkLoad = true;

    // Start is called before the first frame update
    void Awake ()
    {
        gapBetweenChunks = new Vector3 (0, 0, 48f);

        //Chunks = new List<GameObject> ();
    }

    private void Start ()
    {
        startingPos = firstChunk.transform.position;
        InitialChunkLoad ();
    }

    //Adds Chunk to list
    public void Add (GameObject go)
    {
        //Chunks.Add (go);
    }

    public void ActivateNextPath ()
    {

        playerOnChunk++;

        if (chunkIterator == Chunks.Count - 1)
        {
            Chunks[chunkIterator].transform.position = startingPos + (gapBetweenChunks * (playerOnChunk + Chunks.Count - 1));
            chunkIterator = 0;

        }
        else
        {
            Chunks[chunkIterator].transform.position = startingPos + (gapBetweenChunks * (playerOnChunk + Chunks.Count - 1));
            chunkIterator++;

        }

        Chunks[chunkIterator].GetComponent<ChunkScript> ().SetAllActive ();

    }

    public void InitialChunkLoad ()
    {

        //RandomizeList (Chunks);
        int rep = 1;

        foreach (GameObject chunkGO in Chunks)
        {
            //Set active
            chunkGO.SetActive (true);
            chunkGO.GetComponent<ChunkScript> ().SetAllActive ();

            chunkGO.transform.position = startingPos + (gapBetweenChunks * rep);

            rep++;
        }

        //used to count place in List
        chunkIterator = 0;
        playerOnChunk = 1;

    }

    public void ChunkManagerRestart ()
    {

        if (firstChunkLoad)
        {
            firstChunkLoad = false;
        }
        else InitialChunkLoad ();

    }

    public static void RandomizeList<T> (List<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range (i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }

    }
}
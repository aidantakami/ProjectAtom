﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    //List of chunks
    private List<GameObject> Chunks;

    private int playerOnChunk = 0;
    private int chunkIterator = 0;
    private Vector3 gapBetweenChunks;

    public GameObject firstChunk;
    private Vector3 startingPos;

    // Start is called before the first frame update
    void Awake ()
    {
        gapBetweenChunks = new Vector3 (0, 0, 48f);

        Chunks = new List<GameObject> ();
    }

    private void Start ()
    {
        startingPos = firstChunk.transform.position;
        InitialChunkLoad ();
    }

    //Adds Chunk to list
    public void Add (GameObject go)
    {
        Chunks.Add (go);
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

        int rep = 0;

        foreach (GameObject chunkGO in Chunks)
        {
            //Set active
            chunkGO.SetActive (true);
            chunkGO.GetComponent<ChunkScript> ().SetAllActive ();

            if (rep > 0)
            {
                chunkGO.transform.position = startingPos + (gapBetweenChunks * rep);
            }
            else
            {
                chunkGO.transform.position = startingPos;
            }

            rep++;
        }

        //used to count place in List
        chunkIterator = 0;
        playerOnChunk = 0;

    }

    public void ChunkManagerRestart ()
    {
        playerOnChunk = 0;
        chunkIterator = 0;
        InitialChunkLoad ();
    }

}
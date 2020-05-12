using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    //List of chunks
    private List<GameObject> Chunks;

    private int playerOnChunk = 0;
    private int chunkIterator = 0;
    private Vector3 gapBetweenChunks;
    private Vector3 startingPos;

    // Start is called before the first frame update
    void Awake()
    {
        gapBetweenChunks = new Vector3(0, 0, 500);

        Chunks = new List<GameObject>();
    }

    private void Start()
    {
        startingPos = Chunks[0].transform.position;
        InitialChunkLoad();
    }

    //Adds Chunk to list
    public void Add(GameObject go)
    {
        Chunks.Add(go);
    }

    public void ActivateNextPath()
    {
        Debug.Log("Path activated " + Time.deltaTime);

        //Increments what chunk player is on
        playerOnChunk++;

        //Sets old chunk inactive
        Chunks[chunkIterator - 1].SetActive(false);


        //If end of chunk list
        if (chunkIterator == Chunks.Count - 1)
        {
            chunkIterator = 0;
            Chunks[chunkIterator].SetActive(true);
            Chunks[chunkIterator].transform.position = startingPos + (gapBetweenChunks * playerOnChunk);
        }
        //else
        else
        {
            chunkIterator++;
            Chunks[chunkIterator].SetActive(true);
            Chunks[chunkIterator].transform.position = startingPos + (gapBetweenChunks * playerOnChunk);
        }

    }

    public void InitialChunkLoad()
    {

        //Loads the first 2 chunks
        for(int rep = 0; rep < Chunks.Count; rep++)
        {
            //For first 2
            if(rep < 2)
            {
                //Set active
                Chunks[rep].SetActive(true);

                //Move second back behind 1
                if(rep == 1)
                {
                    Chunks[rep].transform.position = startingPos + gapBetweenChunks;
                }

            }
            //Otherwise, inactive, set to starting position
            else
            {
                Chunks[rep].transform.position = startingPos;
                Chunks[rep].SetActive(false);
            }

            //used to count place in List
            chunkIterator = 1;
            playerOnChunk = 1;

        }
    }

    public void ChunkManagerRestart()
    {
        playerOnChunk = 0;
        chunkIterator = 0;
        InitialChunkLoad();
    }
}

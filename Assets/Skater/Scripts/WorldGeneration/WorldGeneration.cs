using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{




    // GamePlay
    private float chunkSpawnZ;
    private Queue<Chunk> activeChunks = new Queue<Chunk>();
    private List<Chunk> chunkPool = new List<Chunk>();


    // Configurable Fields
    [SerializeField] private int firstChunkSpawnPosition = -10;
    [SerializeField] private int chunkOnScreen = 5;
    [SerializeField] private float deSpawnDistance = 5.0f;

    [SerializeField] private List<GameObject> chunkPrefab;
    [SerializeField] private Transform cameraTransform;

    #region TO DELETE $$
    private void Awake()
    {
        ResetWorld();
    }
    #endregion
    private void Start()
    {
        // Check if we have an empty prefab list
        if (chunkPrefab.Count == 0)
        {
            Debug.LogError("No chunk Prefab found on the WorldGenerator, Please assign some chunks");
            return;
        }
        // try to assign camera
        if (!cameraTransform)
        {
            cameraTransform = Camera.main.transform;
            Debug.Log(" Assigned camera transform Automatically");

        }

    }


    private void Update()
    {
        ScanPosition();
    }


    private void ScanPosition()
    {
        float cameraZ = cameraTransform.position.z;
        Chunk lastChunk = activeChunks.Peek();


        if (cameraZ >= lastChunk.transform.position.z + lastChunk.chunkLength + deSpawnDistance)
        {
            SpawnNewChunk();
            DeleteLastChunk();
        }
    }

    private void SpawnNewChunk()
    {
        // GET rndom index of prefab to spawn
        int randomIndex = Random.Range(0, chunkPrefab.Count);


        // does it already exist in pool
        Chunk chunk = chunkPool.Find(x => !x.gameObject.activeSelf && x.name == (chunkPrefab[randomIndex].name + "(Clone)"));

        // Create a chunk if we could not find one
        if (!chunk)
        {
            GameObject go = Instantiate(chunkPrefab[randomIndex], transform);
            chunk = go.GetComponent<Chunk>();
        }

        // place the object and show it
        chunk.transform.position = new Vector3(0, 0, chunkSpawnZ);
        chunkSpawnZ += chunk.chunkLength;


        // Store the value to reuse in pool
        activeChunks.Enqueue(chunk);
        chunk.ShowChunk();

    }

    private void DeleteLastChunk()
    {
        Chunk chunk = activeChunks.Dequeue();
        chunk.HideChunk();
        chunkPool.Add(chunk);
    }

    public void ResetWorld()
    {
        // Reset the chunk SpawnZ
        chunkSpawnZ = firstChunkSpawnPosition;
        for (int i= activeChunks.Count; i != 0; i--)
        {
            DeleteLastChunk();
        }

        for (int i = 0; i < chunkOnScreen; i++)
        {
            SpawnNewChunk();
        }


    }



}

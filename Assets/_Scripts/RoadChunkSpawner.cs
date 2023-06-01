
using System.Collections.Generic;
using UnityEngine;

public class RoadChunkSpawner : MonoBehaviour
{
    public RoadChunk chunk0;
    public RoadChunk chunk1;
    public RoadChunk chunk2;
    public RoadChunk chunk3;

    private Vector3 startPosition = new Vector3(0, 0, -5);
    private Vector3 nextChunkPosition;
    private float chunkLength = 50f;
    private int maxChunksOnStart = 2;
    private float timeSinceLastChunkSpawn;

    private ObjectPool<RoadChunk> chunk0Pool;
    private ObjectPool<RoadChunk> chunk1Pool;
    private ObjectPool<RoadChunk> chunk2Pool;
    private ObjectPool<RoadChunk> chunk3Pool;


    private void Start()
    {
        RoadChunk.OnNextChunkTrigger += OnNextChunkTrigger;
        InstantiateChunkPools();
        timeSinceLastChunkSpawn = Time.time;
    }

    private void OnDisable()
    {
        RoadChunk.OnNextChunkTrigger -= OnNextChunkTrigger;

    }
    private void InitializeFirstChunks()
    {
        nextChunkPosition = startPosition;
        for (int i = 0; i < maxChunksOnStart; i++)
        {
            var chunk = GetRandomRoadChunk();
            chunk.ChunkPositionSetup(nextChunkPosition, true);
            nextChunkPosition.z += chunkLength;
        }        
    }


    private void OnNextChunkTrigger(Vector3 pos)
    {
        if (Time.time - timeSinceLastChunkSpawn < 1f)
            return;

        timeSinceLastChunkSpawn = Time.time;
        var chunk = GetRandomRoadChunk();
        chunk.ChunkPositionSetup(nextChunkPosition);
        nextChunkPosition.z += chunkLength;
    }

    private void InstantiateChunkPools()
    {
        chunk0Pool = new ObjectPool<RoadChunk>(chunk0, 2, transform);
        chunk1Pool = new ObjectPool<RoadChunk>(chunk1, 2, transform);
        chunk2Pool = new ObjectPool<RoadChunk>(chunk2, 2, transform);
        chunk3Pool = new ObjectPool<RoadChunk>(chunk3, 2, transform);

        InitializeFirstChunks();
    }
    private RoadChunk GetRandomRoadChunk()
    {
        int randomPool = Random.Range(0, 4);
        RoadChunk chunk = null;
        switch (randomPool)
        {
            case 0:
                chunk = chunk0Pool.GetFreeElement();
                break;
            case 1:
                chunk = chunk1Pool.GetFreeElement();
                break;
            case 2:
                chunk = chunk2Pool.GetFreeElement();
                break;
            case 3:
                chunk = chunk3Pool.GetFreeElement();
                break;
        }
        return chunk;
    }
}

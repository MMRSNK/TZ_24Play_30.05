using System;
using System.Collections;
using UnityEngine;

public class RoadChunk : MonoBehaviour
{
    private Vector3 chunkPosition;
    public static Action<Vector3> OnNextChunkTrigger;

    private bool _isMoving = false;
    public float ChunkMoveSpeed;

    private void Start()
    {
        RoadChunk.OnNextChunkTrigger += CheckForDisableChunk;
    }
    private void OnDisable()
    {
        RoadChunk.OnNextChunkTrigger -= CheckForDisableChunk;
    }

    private void Update()
    {
        if (_isMoving)
        {
            MoveChunkToPlace(chunkPosition);
        }
    }

    private void MoveChunkToPlace(Vector3 chunkPosition)
    {
        if (!_isMoving)
            return;

        if(transform.position.y > chunkPosition.y)
        {
            transform.position = chunkPosition;
            _isMoving = false;
            return;
        }
        
        transform.Translate(Vector3.up * ChunkMoveSpeed * Time.deltaTime);
    }

    private void CheckForDisableChunk(Vector3 position)
    {
        if (chunkPosition.z < position.z)
            StartCoroutine(DisableChunk());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnNextChunkTrigger?.Invoke(chunkPosition);
        }
    }
    public void ChunkPositionSetup(Vector3 position, bool fastPlacing = false)
    {
        chunkPosition = position;

        if (fastPlacing)
        {
            transform.position = chunkPosition;
            return;
        }

        transform.position = new Vector3(chunkPosition.x, chunkPosition.y - 20f, chunkPosition.z);
        _isMoving = true;
    }
    private IEnumerator DisableChunk()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}

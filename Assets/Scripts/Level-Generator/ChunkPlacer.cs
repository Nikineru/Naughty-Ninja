using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Chunk start_chunk;
    [SerializeField] Chunk[] chunks_prefabs;

    private List<Chunk> spawned_chunks = new List<Chunk>();
    private const  int max_chunks_count = 3;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        spawned_chunks.Add(start_chunk);
    }

    private void Update()
    {
        if (_camera.IsSeeingPoint(spawned_chunks[spawned_chunks.Count - 1].End.position))
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk() 
    {
        Chunk new_chunk = Instantiate(chunks_prefabs[Random.Range(0, chunks_prefabs.Length)]);
        new_chunk.transform.position = spawned_chunks[spawned_chunks.Count - 1].End.position - new_chunk.Begin.localPosition;
        spawned_chunks.Add(new_chunk);

        if (spawned_chunks.Count >= max_chunks_count)
        {
            Destroy(spawned_chunks[0]);
            spawned_chunks.RemoveAt(0);
        }
    }
}
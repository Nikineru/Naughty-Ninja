using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Chunk start_chunk;
    [SerializeField] private AnimationCurve ProgressByDistance;
    [SerializeField] Chunk[] chunks_prefabs;

    private List<Chunk> spawned_chunks = new List<Chunk>();
    private const  int max_chunks_count = 4;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        spawned_chunks.Add(start_chunk);
    }

    private void Update()
    {
        if (_camera.IsSeeingPointHorizontal(spawned_chunks[spawned_chunks.Count - 1].End.position, margin: 1.5f))
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk() 
    {
        Chunk new_chunk = Instantiate(GetRandomChunk());
        new_chunk.transform.position = spawned_chunks[spawned_chunks.Count - 1].End.position - new_chunk.Begin.localPosition;
        spawned_chunks.Add(new_chunk);

        if (spawned_chunks.Count >= max_chunks_count)
        {
            Destroy(spawned_chunks[0].gameObject);
            spawned_chunks.RemoveAt(0);
        }
    }

    private Chunk GetRandomChunk() 
    {
        float progress = ProgressByDistance.Evaluate(player.transform.position.x);
        List<float> chances = chunks_prefabs.Select(i => i.ChanceByDistanse.Evaluate(progress)).ToList();

        float value = Random.Range(0, chances.Sum());
        float sum = 0;

        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];

            if (value < sum)
            {
                return chunks_prefabs[i];
            }
        }

        return chunks_prefabs[chunks_prefabs.Count() - 1];
    }
}
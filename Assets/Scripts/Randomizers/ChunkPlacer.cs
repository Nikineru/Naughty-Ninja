using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CameraExstentions;

public class ChunkPlacer : Pool<Chunk>
{
    [SerializeField] private Transform player;
    [SerializeField] private Chunk start_chunk;
    [SerializeField] private AnimationCurve progress_by_distance;
    [SerializeField] Chunk[] chunks_prefabs;

    private List<Chunk> spawned_chunks = new List<Chunk>();
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        spawned_chunks.Add(start_chunk);
    }
    private void Update()
    {
        if (spawned_chunks.Count == 0)
            return;

        if (_camera.IsSeeingPointHorizontal(spawned_chunks[spawned_chunks.Count - 1].End.position, margin: -5, pattern: CheckPatterns.MaxSide))
        {
            SpawnChunk();
        }
        if (_camera.IsSeeingPointHorizontal(spawned_chunks[0].End.position, margin: -5f, pattern: CheckPatterns.MinSide) == false) 
        {
            Push(spawned_chunks[0]);
        }
    }

    private void SpawnChunk() 
    {
        Chunk random_chunk = GetRandomChunk();
        Chunk chunk_instance = Pull(random_chunk);

        if (chunk_instance == null || chunk_instance.transform == null)
        {
            chunk_instance = Instantiate(random_chunk);
        }

        chunk_instance.transform.position = spawned_chunks[spawned_chunks.Count - 1].End.position - chunk_instance.Begin.localPosition;
        spawned_chunks.Add(chunk_instance);
    }
    private Chunk GetRandomChunk()
    {
        float progress = progress_by_distance.Evaluate(player.transform.position.x);
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
    protected override void PushIternal(Chunk target)
    {
        spawned_chunks.RemoveAt(0);
    }
}
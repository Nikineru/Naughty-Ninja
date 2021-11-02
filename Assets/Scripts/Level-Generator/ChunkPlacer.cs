using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Chunk start_chunk;
    [SerializeField] private AnimationCurve progress_by_distance;
    [SerializeField] Chunk[] chunks_prefabs;

    private List<Chunk> spawned_chunks = new List<Chunk>();
    private List<Chunk> chunks_pool = new List<Chunk>();
    private const int MAX_CHUNKS_COUNT = 4;
    private const int MAX_POOL_SIZE = 4;
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
        Chunk random_chunk = GetRandomChunk();
        Chunk chunk_instance = PullChunk(random_chunk);

        if (chunk_instance == null || chunk_instance.transform == null)
        {
            chunk_instance = Instantiate(random_chunk);
        }

        chunk_instance.transform.position = spawned_chunks[spawned_chunks.Count - 1].End.position - chunk_instance.Begin.localPosition;
        spawned_chunks.Add(chunk_instance);

        if (spawned_chunks.Count >= MAX_CHUNKS_COUNT)
        {
            PushChunk(spawned_chunks[0]);
        }
    }
    private void PushChunk(Chunk chunk)
    {
        //chunk.gameObject.SetActive(false);
        spawned_chunks.RemoveAt(0);
        chunks_pool.Add(chunk);

        if (chunks_pool.Count() > MAX_POOL_SIZE) 
        {
            Destroy(chunks_pool[0].gameObject);
            chunks_pool.RemoveAt(0);
        }
    }

    private Chunk PullChunk(Chunk chunk) 
    {
        Chunk chunk_instance = chunks_pool.FirstOrDefault(i => i.ID == chunk.ID);

        if (chunk_instance == null)
            return null;

        //chunk_instance.gameObject.SetActive(true);
        chunks_pool.Remove(chunk_instance);
        return chunk_instance;
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
}
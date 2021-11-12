using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static CameraExstentions;

public class RandomObjectsPlacer : Pool<RandomlyInitialized>
{
    [SerializeField] private float spawn_range;
    [SerializeField] private float spawn_delay;
    [SerializeField] private RandomlyInitialized[] prefabs;

    private List<RandomlyInitialized> spawned_objects = new List<RandomlyInitialized>();
    private const float accuracy =  10;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        InvokeRepeating(nameof(Spawn), 0, spawn_delay);
    }
    
    private void Spawn()
    {
        RefreshPool();

        RandomlyInitialized random_prefab = prefabs[Random.Range(0, prefabs.Length)];

        Vector3 initialize_position = Vector3.zero;

        if (random_prefab.GetInitializePosition(ref initialize_position, spawn_range, accuracy)) 
        {
            RandomlyInitialized instance = Pull(random_prefab);

            if (instance == null) 
            {
                instance = Instantiate(random_prefab);
            }

            instance.transform.position = initialize_position;
            spawned_objects.Add(instance);
        }
    }
    private void RefreshPool()
    {
        if (spawned_objects.Count == 0)
            return;

        RandomlyInitialized first_object = spawned_objects[0];
        Vector2 check_position = first_object.transform.CalculateWorldBounds().max;

        if (_camera.IsSeeingPointHorizontal(check_position, pattern: CheckPatterns.MinSide) == false) 
        {
            Push(first_object);
        }
    }

    protected override void PullIternal(RandomlyInitialized target, RandomlyInitialized instance)
    {
        instance.gameObject.SetActive(true);
    }

    protected override void PushIternal(RandomlyInitialized target)
    {
        target.gameObject.SetActive(false);
        spawned_objects.RemoveAt(0);
    }
}
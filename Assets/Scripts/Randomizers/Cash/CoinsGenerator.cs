using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    [SerializeField] private float spawn_range;
    [SerializeField] private float spawn_delay;
    [SerializeField] private RandomlyInitialized[] coins_prefabs;

    private const float accuracy =  10;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        InvokeRepeating(nameof(SpawnCoins), 0, spawn_delay);
    }

    private void SpawnCoins()
    {
        RandomlyInitialized random_prefab = coins_prefabs[Random.Range(0, coins_prefabs.Length)];
        Vector3 initialize_position = Vector3.zero;

        if (random_prefab.GetInitializePosition(ref initialize_position, spawn_range, accuracy))
            Instantiate(random_prefab, initialize_position, Quaternion.identity);
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    [SerializeField] private float spawn_range;
    [SerializeField] private float spawn_delay;
    [SerializeField] private CoinsPattern[] coins_prefabs;

    private List<CoinsPattern> coins_pool;
    private const float accuracy = 10;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;

        foreach (CoinsPattern pattern in coins_prefabs)
        {
            CoinsPattern initialization = Instantiate(pattern);
            initialization.gameObject.SetActive(false);
            coins_pool.Add(initialization);
        }

        InvokeRepeating(nameof(SpawnCoins), 0, spawn_delay);
    }

    private void SpawnCoins()
    {
        Vector2 right_borded_position = _camera.ViewportToWorldPoint(new Vector2(1, 0.5f));
        float spawn_shift = spawn_range / 2;

        RaycastHit2D top_hit = Physics2D.Raycast(right_borded_position, Vector2.up, distance: spawn_shift);
        RaycastHit2D bottom_hit = Physics2D.Raycast(right_borded_position, Vector2.down, distance: spawn_shift);

        float max_height = top_hit.transform == null ? right_borded_position.y + spawn_shift : top_hit.point.y;
        float min_height = bottom_hit.transform == null ? right_borded_position.y - spawn_shift : bottom_hit.point.y;

        CoinsPattern pattern_prefab = coins_prefabs[Random.Range(0, coins_prefabs.Length)];
        Bounds pattern_bounds = pattern_prefab.transform.CalculateLocalBounds();

        max_height -= pattern_bounds.max.y;
        min_height -= pattern_bounds.min.y;

        List<Vector2> possible_positions = new List<Vector2>();

        for (float height = min_height; height < max_height; height += pattern_bounds.size.y / accuracy)
        {
            Vector2 pattern_position = new Vector2(right_borded_position.x, height);
            Bounds new_bound = new Bounds(pattern_bounds.center + (Vector3)pattern_position, pattern_bounds.size);

            if (Physics2D.BoxCastAll(new_bound.center, new_bound.size, 0f, Vector2.zero).Where(i => i.transform.parent != pattern_prefab.transform).Count() == 0) 
            {
                possible_positions.Add(pattern_position);
            }
        }

        if(possible_positions.Count > 0) 
        {
            Instantiate(pattern_prefab, possible_positions[Random.Range(0, possible_positions.Count)], Quaternion.identity);
        }
    }
}
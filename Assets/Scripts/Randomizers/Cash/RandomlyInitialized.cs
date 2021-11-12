using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomlyInitialized : PoolObject
{
    private Camera _camera;

    private void OnValidate()
    {
        _camera = Camera.main;
        CalculateID();
    }

    public bool GetInitializePosition(ref Vector3 initialize_position, float spawn_range, float accuracy=5) 
    {
        Vector2 right_borded_position = _camera.ViewportToWorldPoint(new Vector2(1, 0.5f));
        float spawn_shift = spawn_range / 2;

        RaycastHit2D top_hit = Physics2D.Raycast(right_borded_position, Vector2.up, distance: spawn_shift);
        RaycastHit2D bottom_hit = Physics2D.Raycast(right_borded_position, Vector2.down, distance: spawn_shift);

        float max_height = top_hit.transform == null ? right_borded_position.y + spawn_shift : top_hit.point.y;
        float min_height = bottom_hit.transform == null ? right_borded_position.y - spawn_shift : bottom_hit.point.y;

        Bounds local_pattern_bounds = transform.CalculateLocalBounds();

        max_height -= local_pattern_bounds.max.y;
        min_height -= local_pattern_bounds.min.y;

        List<Vector2> possible_positions = new List<Vector2>();
        float height_step = (max_height - min_height) / accuracy;

        for (float height = min_height; height < max_height; height += height_step)
        {
            Vector2 initialize_position_variant = new Vector2(right_borded_position.x, height);
            Vector2 check_position = initialize_position_variant + (Vector2)local_pattern_bounds.center;

            if (Physics2D.BoxCastAll(check_position, local_pattern_bounds.size, 0f, Vector2.zero).Where(i => i.transform.parent != transform).Count() == 0)
            {
                possible_positions.Add(initialize_position_variant);
            }
        }

        if (possible_positions.Count > 0)
        {
            initialize_position = possible_positions[Random.Range(0, possible_positions.Count)];
            return true;
        }

        return false;
    }
}
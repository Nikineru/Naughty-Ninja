using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    [SerializeField] private float spawn_range;
    [SerializeField] private float spawn_delay;
    [SerializeField] private CoinsPattern[] coins_prefabs;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
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

        CoinsPattern coins_prefab = coins_prefabs[Random.Range(0, coins_prefabs.Length)];
        var borders_shifts = coins_prefab.GetHorizontalBordersShift();

        max_height -= borders_shifts.Top;
        min_height += borders_shifts.Bottom;

        if (min_height > max_height)
            return;

        float curret_height = Random.Range(max_height, min_height);
        Instantiate(coins_prefab, new Vector2(right_borded_position.x, min_height), Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        if (_camera == null) 
        {
            print("Not found Camera");
            return;
        }

        Vector2 right_borded_position = _camera.ViewportToWorldPoint(new Vector2(1, 0.5f));
        float spawn_shift = spawn_range / 2;

        RaycastHit2D top_hit = Physics2D.Raycast(right_borded_position, Vector2.up, distance: spawn_shift);
        RaycastHit2D bottom_hit = Physics2D.Raycast(right_borded_position, Vector2.down, distance: spawn_shift);

        float max_height = top_hit.transform == null ? right_borded_position.y + spawn_shift : top_hit.point.y;
        float min_height = bottom_hit.transform == null ? right_borded_position.y - spawn_shift : bottom_hit.point.y;
        float curret_height = Random.Range(max_height, min_height);

        Gizmos.DrawSphere(new Vector2(right_borded_position.x, min_height), 1);
        Gizmos.DrawSphere(new Vector2(right_borded_position.x, max_height), 1);
    }
}

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
        var borders_shifts = coins_prefab.GetBordersShift();

        max_height -= borders_shifts.Top;
        min_height += borders_shifts.Bottom;

        if (min_height > max_height)
            return;

        Vector2 patter_position = new Vector2(right_borded_position.x, Random.Range(max_height, min_height));

        RaycastHit2D right_hit = Physics2D.Raycast(patter_position, Vector2.right);
        RaycastHit2D left_hit = Physics2D.Raycast(patter_position, Vector2.left);

        //print($"Borders: {borders_shifts.Left} {borders_shifts.Right}");
        //print($"Casts: {patter_position.Distance(left_hit.point)} {patter_position.Distance(right_hit.point)}");

        if (right_hit.transform != null && patter_position.Distance(right_hit.point) < borders_shifts.Right) 
        {
            //print("Wrong Width");
            return;
        }

        if (left_hit.transform == null && patter_position.Distance(left_hit.point) < borders_shifts.Left)
        {
            //print("Wrong Width");
            return;
        }

        Instantiate(coins_prefab, patter_position, Quaternion.identity);
    }

    ////private void OnDrawGizmos()
    //{
    //    if (_camera == null) 
    //        return;

    //    Vector2 right_borded_position = _camera.ViewportToWorldPoint(new Vector2(1, 0.5f));
    //    float spawn_shift = spawn_range / 2;

    //    RaycastHit2D top_hit = Physics2D.Raycast(right_borded_position, Vector2.up, distance: spawn_shift);
    //    RaycastHit2D bottom_hit = Physics2D.Raycast(right_borded_position, Vector2.down, distance: spawn_shift);

    //    float max_height = top_hit.transform == null ? right_borded_position.y + spawn_shift : top_hit.point.y;
    //    float min_height = bottom_hit.transform == null ? right_borded_position.y - spawn_shift : bottom_hit.point.y;

    //    CoinsPattern coins_prefab = coins_prefabs[Random.Range(0, coins_prefabs.Length)];
    //    var borders_shifts = coins_prefab.GetBordersShift();

    //    Gizmos.color = Color.red;

    //    Gizmos.DrawSphere(new Vector2(right_borded_position.x, max_height), 1);
    //    Gizmos.DrawSphere(new Vector2(right_borded_position.x, min_height), 1);

    //    max_height -= borders_shifts.Top;
    //    min_height += borders_shifts.Bottom;

    //    if (min_height > max_height)
    //        return;

    //    Vector2 patter_position = new Vector2(right_borded_position.x, min_height);

    //    RaycastHit2D right_hit = Physics2D.Raycast(patter_position, Vector2.right);
    //    RaycastHit2D left_hit = Physics2D.Raycast(patter_position, Vector2.left);

    //    Gizmos.color = Color.red;

    //    if (right_hit.transform != null)
    //        Gizmos.DrawSphere(right_hit.point, 1);
    //    if (left_hit.transform != null)
    //        Gizmos.DrawSphere(left_hit.point, 1);
    //}
}
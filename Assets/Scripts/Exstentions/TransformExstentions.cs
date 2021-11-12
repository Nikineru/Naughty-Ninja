using System.Collections.Generic;
using UnityEngine;

public static class TransformExstentions
{
    public static Vector2 Direction(this Vector2 position, Vector2 target) 
    {
        return (target - position).normalized;
    }
    public static Vector2 Direction(this Vector3 position, Vector3 target)
    {
        return (target - position).normalized;
    }
    public static Vector3 ClampDirection(this Vector3 position, Vector3 target)
    {
        Vector3 direction = Direction(position, target);

        HardClamp(ref direction.x);
        HardClamp(ref direction.y);
        HardClamp(ref direction.z);

        return direction;
    }

    public static void HardClamp(ref float value)
    {
        if (value > 0)
            value = 1;
        else if (value < 0)
            value = -1;
        else
            value = 0;
    }

    public static Vector2 Randomize(this Vector2 target) 
    {
        target.x = Random.Range(-target.x, target.x);
        target.y = Random.Range(-target.y, target.y);

        return target;
    }
    public static Vector2 Randomize(this Vector3 target)
    {
        target.x = Random.Range(-target.x, target.x);
        target.y = Random.Range(-target.y, target.y);
        target.z = Random.Range(-target.z, target.z);

        return target;
    }

    public static Vector2 RandomOffset(this Vector2 target, Vector2 offset)
    {
        offset = offset.Randomize();
        return target + offset;
    }
    public static Vector3 RandomOffset(this Vector3 target, Vector3 offset)
    {
        offset = offset.Randomize();
        return target + offset;
    }

    public static float Distance(this Vector2 position, Vector2 target)
    {
        return (position - target).magnitude;
    }
    public static float Distance(this Vector3 position, Vector3 target)
    {
        return (position - target).magnitude;
    }

    public static Vector2 GetMaximizedVector(Vector2 first, Vector2 second) 
    {
        Vector2 maximized_vector = Vector2.zero;

        maximized_vector.x = Mathf.Max(first.x, second.x);
        maximized_vector.y = Mathf.Max(first.y, second.y);

        return maximized_vector;
    }
    public static Vector2 GetMinimizedVector(Vector2 first, Vector2 second)
    {
        Vector2 minimized_vector = Vector2.zero;

        minimized_vector.x = Mathf.Min(first.x, second.x);
        minimized_vector.y = Mathf.Min(first.y, second.y);

        return minimized_vector;
    }

    public static Bounds CalculateLocalBounds(this Transform target)
    {
        Bounds bounds = new Bounds();

        foreach (Transform child in target.GetComponentInChildren<Transform>())
        {
            Bounds child_bounds = new Bounds(child.localPosition, child.localScale);

            bounds.max = GetMaximizedVector(bounds.max, child_bounds.max);
            bounds.min = GetMinimizedVector(bounds.min, child_bounds.min);
        }

        return bounds;
    }
    public static Bounds CalculateWorldBounds(this Transform target)
    {
        Bounds bounds = target.CalculateLocalBounds();
        bounds.center = target.TransformPoint(bounds.center);
        return bounds;
    }

    public static IEnumerable<Transform> GetAllParent(this Transform target) 
    {
        while(target.parent != null) 
        {
            target = target.parent;
            yield return target;
        }
    }
}
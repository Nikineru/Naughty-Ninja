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

    public static float Distance(this Vector2 position, Vector2 target)
    {
        return (position - target).magnitude;
    }

    public static float Distance(this Vector3 position, Vector3 target)
    {
        return (position - target).magnitude;
    }
}

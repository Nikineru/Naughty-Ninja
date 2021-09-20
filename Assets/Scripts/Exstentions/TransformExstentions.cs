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

    public static float Distance(this Vector2 position, Vector2 target)
    {
        return (position - target).magnitude;
    }

    public static float Distance(this Vector3 position, Vector3 target)
    {
        return (position - target).magnitude;
    }
}

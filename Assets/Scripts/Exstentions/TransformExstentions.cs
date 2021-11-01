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
}

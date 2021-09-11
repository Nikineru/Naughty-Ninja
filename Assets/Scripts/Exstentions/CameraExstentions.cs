using UnityEngine;

public static class CameraExstentions
{
    public static bool IsSeeingPoint(this Camera camera, Vector3 point)
    {
        Vector3 viewPos = camera.WorldToViewportPoint(point);
        return (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0);
    }
}
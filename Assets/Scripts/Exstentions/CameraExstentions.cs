using UnityEngine;

public static class CameraExstentions
{
    public enum CheckPatterns 
    {
        BothSides,
        MinSide,
        MaxSide
    }

    public static bool IsSeeingPoint(this Camera camera, Vector3 point, Vector2? margin=null, CheckPatterns horizontal_pattern = CheckPatterns.BothSides, CheckPatterns vertical_pattern = CheckPatterns.BothSides)
    {
        if (margin != null)
            point += (Vector3)margin;

        return camera.IsSeeingPointHorizontal(point, pattern: horizontal_pattern) && camera.IsSeeingPointVertical(point, pattern:vertical_pattern);
    }

    public static bool IsSeeingPointHorizontal(this Camera camera, Vector3 point, float margin=0, CheckPatterns pattern = CheckPatterns.BothSides)
    {
        point.x += margin;
        Vector3 viewPos = camera.WorldToViewportPoint(point);

        switch (pattern) 
        {
            case CheckPatterns.BothSides:
                return viewPos.x >= 0 && viewPos.x <= 1;
            case CheckPatterns.MinSide:
                return viewPos.x >= 0;
            case CheckPatterns.MaxSide:
                return viewPos.x <= 1;

        }

        return false;
    }

    public static bool IsSeeingPointVertical(this Camera camera, Vector3 point, float margin = 0, CheckPatterns pattern = CheckPatterns.BothSides)
    {
        point.y += margin;
        Vector3 viewPos = camera.WorldToViewportPoint(point);

        switch (pattern)
        {
            case CheckPatterns.BothSides:
                return viewPos.y >= 0 && viewPos.y <= 1;
            case CheckPatterns.MinSide:
                return viewPos.y >= 0;
            case CheckPatterns.MaxSide:
                return viewPos.y <= 1;

        }

        return false;
    }

    public static Bounds LocalBounds(this Camera camera) 
    {
        Vector2 camera_size = camera.ViewportToWorldPoint(new Vector2(1, 1)) - camera.ViewportToWorldPoint(new Vector2(0, 0));
        return new Bounds(Vector2.zero, camera_size);
    }

    public static Bounds WorldBounds(this Camera camera) 
    {
        Bounds bounds = camera.LocalBounds();
        bounds.center = camera.transform.TransformPoint(bounds.center);

        return bounds;
    }
}
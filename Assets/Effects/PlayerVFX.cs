using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    public Color GetPixel(Vector2 local_position, SpriteRenderer renderer)
    {
        Vector2Int direction = GetClampVector(Vector2.zero - local_position);

        local_position.x = Mathf.Clamp(local_position.x, -0.5f, 0.5f);
        local_position.y = Mathf.Clamp(local_position.y, -0.5f, 0.5f);

        local_position += new Vector2(0.5f, 0.5f);

        Color[] pixels = renderer.sprite.texture.GetPixels();

        int width = renderer.sprite.texture.width;
        int height = renderer.sprite.texture.height;

        int x = (int)(local_position.x * width);
        int y = (int)(local_position.y * height);

        x = Mathf.Clamp(x, 0, width - 1);
        y = Mathf.Clamp(y, 0, height - 1);

        Vector2Int pixel_coordinates = new Vector2Int(x, y);

        while (pixels[pixel_coordinates.y * width + pixel_coordinates.x].a < 1)
            pixel_coordinates += direction;

        return pixels[pixel_coordinates.y * width + pixel_coordinates.x];
    }
    private Vector2Int GetClampVector(Vector2 target)
    {
        int x = target.x > 0 ? 1 : -1;
        int y = target.y > 0 ? 1 : -1;

        return new Vector2Int(x, y);
    }
}

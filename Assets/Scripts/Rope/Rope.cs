using UnityEngine;
using UnityEngine.Events;

public class Rope : MonoBehaviour
{
    public bool IsGrappling { get; private set; }
    public bool IsDrawing { get; private set; }

    [Header("Events")]
    [SerializeField] private UnityEvent grabbed;
    [SerializeField] private UnityEvent absolved;

    [Header("Refrences:")]
    [SerializeField] private ParticleSystem visual_effect;

    [Header("Settings:")]
    [SerializeField] private int percision;
    [Range(0, 100)] [SerializeField] private float straightening_speed;

    [Header("Animation:")]
    public AnimationCurve rope_curve;
    [SerializeField] [Range(0.01f, 4)] private float waves_size;
    private float curret_waves_size;

    [Header("Rope Speed:")]
    public AnimationCurve launch_speed_curve;
    [SerializeField] [Range(1, 50)] private float launch_speed = 4;

    private float move_timer = 0;
    private LineRenderer rope_renderer;
    private bool is_straight_rope = true;

    private Transform start_point;
    private RaycastHit2D grabbed_hit;

    private void Awake()
    {
        rope_renderer = GetComponent<LineRenderer>();
        rope_renderer.enabled = false;
    }
    private void Update()
    {
        if (IsDrawing == false)
            return;

        move_timer += Time.deltaTime;
        DrawRope();
    }

    public void StartGrapp(Transform start_point, RaycastHit2D hit) 
    {
        grabbed_hit = hit;
        this.start_point = start_point;

        move_timer = 0;
        IsDrawing = true;
        is_straight_rope = false;
        curret_waves_size = waves_size;

        rope_renderer.enabled = true;
        rope_renderer.positionCount = percision;
    }
    public void StopDraw() 
    {
        IsDrawing = false;
        IsGrappling = false;
        rope_renderer.enabled = false;
        absolved?.Invoke();
    }
    private void DrawRope()
    {
        if (is_straight_rope)
        {
            if (IsGrappling == false)
            {
                grabbed?.Invoke();
                IsGrappling = true;
                Color effect_color = DefineColor(grabbed_hit.transform.GetComponent<SpriteRenderer>());

                ParticleSystem.MainModule settings = visual_effect.GetComponent<ParticleSystem>().main;
                settings.startColor = effect_color;

                Instantiate(visual_effect, grabbed_hit.point, Quaternion.identity);

            }
            if (curret_waves_size > 0)
            {
                curret_waves_size -= Time.deltaTime * straightening_speed;
                DrawWavesRope();
            }
            else
            {
                curret_waves_size = 0;
                DrawStraightRope();
            }
        }
        else
        {
            if (rope_renderer.GetPosition(percision - 1).x != grabbed_hit.point.x)
            {
                DrawWavesRope();
            }
            else
            {
                is_straight_rope = true;
            }
        }
    }
    private void DrawWavesRope()
    {
        for (int i = 0; i < percision; i++)
        {
            float delta = i / (percision - 1f);
            Vector2 offset = Vector2.Perpendicular(grabbed_hit.point.Direction(start_point.position) * rope_curve.Evaluate(delta) * curret_waves_size);
            Vector2 targetPosition = Vector2.Lerp(start_point.position, grabbed_hit.point, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(start_point.position, targetPosition, launch_speed_curve.Evaluate(move_timer) * launch_speed);

            rope_renderer.SetPosition(i, currentPosition);
        }
    }
    private void DrawStraightRope()
    {
        rope_renderer.positionCount = 2;
        rope_renderer.SetPosition(0, start_point.position);
        rope_renderer.SetPosition(1, grabbed_hit.point);
    }

    #region Color Define
    private Color DefineColor(SpriteRenderer renderer)
    {
        Vector2 local_position = transform.InverseTransformPoint(grabbed_hit.point);
        Color pixel_color = GetPixel(local_position, renderer);
        return pixel_color - (Color.white - renderer.color);
    }
    private Color GetPixel(Vector2 local_position, SpriteRenderer renderer)
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
    #endregion
}
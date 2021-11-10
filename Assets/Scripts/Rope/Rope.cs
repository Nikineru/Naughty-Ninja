using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Rope : MonoBehaviour
{
    [SerializeField] private UnityEvent Grabbed;
    [SerializeField] private UnityEvent Absolved;
    [SerializeField] private ParticleSystem VisualEffect;

    #region fields
    [Header("Refrences:")]
    [SerializeField] private Hook hook;

    [Header("Settings:")]
    [SerializeField] private int percision;
    [Range(0, 100)] [SerializeField] private float straightening_speed;

    [Header("Animation:")]
    public AnimationCurve ropeAnimationCurve;
    [SerializeField] [Range(0.01f, 4)] private float waves_size;
    private float curret_waves_size;

    [Header("Rope Speed:")]
    public AnimationCurve ropeLaunchSpeedCurve;
    [SerializeField] [Range(1, 50)] private float ropeLaunchSpeedMultiplayer = 4;

    private float move_timer = 0;

    public bool isGrappling = false;
    private LineRenderer rope_renderer;
    private bool drawLine = true;
    private bool is_straight_rope = true;
    private Texture2D screen_texture;
    private Camera _camera;
    #endregion

    private void Awake()
    {
        rope_renderer = GetComponent<LineRenderer>();
        rope_renderer.enabled = false;
        screen_texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        _camera = Camera.main;
    }
    private void OnEnable()
    {
        move_timer = 0;
        is_straight_rope = false;
        curret_waves_size = waves_size;

        rope_renderer.enabled = true;
        rope_renderer.positionCount = percision;
    }
    private void OnDisable()
    {
        isGrappling = false;
        rope_renderer.enabled = false;
        Absolved?.Invoke();
    }
    private void Update()
    {
        move_timer += Time.deltaTime;

        if (drawLine)
        {
            DrawRope();
        }
    }

    private void DrawRope()
    {
        if (is_straight_rope)
        {
            if (isGrappling == false)
            {
                hook.Grapple();
                isGrappling = true;
                Color effect_color = DefineColor(hook.GrappedRenderer);

                ParticleSystem.MainModule settings = VisualEffect.GetComponent<ParticleSystem>().main;
                settings.startColor = effect_color;

                Instantiate(VisualEffect, hook.grapplePoint, Quaternion.identity);

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
            if (rope_renderer.GetPosition(percision - 1).x != hook.grapplePoint.x)
            {
                DrawWavesRope();
            }
            else
            {
                is_straight_rope = true;
                Grabbed?.Invoke();
            }
        }
    }
    private void DrawWavesRope()
    {
        for (int i = 0; i < percision; i++)
        {
            float delta = i / (percision - 1f);
            Vector2 offset = Vector2.Perpendicular(hook.DistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * curret_waves_size;
            Vector2 targetPosition = Vector2.Lerp(hook.firePoint.position, hook.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(hook.firePoint.position, targetPosition, ropeLaunchSpeedCurve.Evaluate(move_timer) * ropeLaunchSpeedMultiplayer);

            rope_renderer.SetPosition(i, currentPosition);
        }
    }
    private void DrawStraightRope()
    {
        rope_renderer.positionCount = 2;
        rope_renderer.SetPosition(0, hook.grapplePoint);
        rope_renderer.SetPosition(1, hook.firePoint.position);
    }
    private Color DefineColor(SpriteRenderer renderer)
    {
        Vector2 local_position = transform.InverseTransformPoint(hook.grapplePoint);
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
}
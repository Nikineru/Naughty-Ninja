using UnityEngine;

public class Rope : MonoBehaviour
{
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

    [SerializeField] public bool isGrappling = false;

    private LineRenderer rope_renderer;
    bool drawLine = true;
    bool straightLine = true;
    #endregion

    private void Awake()
    {
        rope_renderer = GetComponent<LineRenderer>();
        rope_renderer.enabled = false;
    }
    private void OnEnable()
    {
        move_timer = 0;
        straightLine = false;
        curret_waves_size = waves_size;

        rope_renderer.enabled = true;
        rope_renderer.positionCount = percision;
    }
    private void OnDisable()
    {
        isGrappling = false;
        rope_renderer.enabled = false;
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
        if (!straightLine)
        {
            if (rope_renderer.GetPosition(percision - 1).x != hook.grapplePoint.x)
            {
                DrawWavesRope();
            }
            else
            {
                straightLine = true;
            }
        }
        else
        {
            if (!isGrappling)
            {
                hook.Grapple();
                isGrappling = true;
                print("Do");
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
}
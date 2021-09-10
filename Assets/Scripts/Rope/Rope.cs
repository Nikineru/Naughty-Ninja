using UnityEngine;

public class Rope : MonoBehaviour
{
    [Header("General refrences:")]
    public Hook grapplingGun;
    [SerializeField] LineRenderer RopeRenderer;

    [Header("General Settings:")]
    [SerializeField] private int percision = 20;
    [Range(0, 100)] [SerializeField] private float straightenLineSpeed = 4;

    [Header("Animation:")]
    public AnimationCurve ropeAnimationCurve;
    [SerializeField] [Range(0.01f, 4)] private float WaveSize = 20;
    float WavesCount;

    [Header("Rope Speed:")]
    public AnimationCurve ropeLaunchSpeedCurve;
    [SerializeField] [Range(1, 50)] private float ropeLaunchSpeedMultiplayer = 4;

    float MoveTimer = 0;

    [SerializeField] public bool isGrappling = false;

    bool drawLine = true;
    bool straightLine = true;


    private void Awake()
    {
        RopeRenderer = GetComponent<LineRenderer>();
        RopeRenderer.enabled = false;

        WavesCount = WaveSize;
    }
    private void OnEnable()
    {
        MoveTimer = 0;
        straightLine = false;
        WavesCount = WaveSize;

        RopeRenderer.enabled = true;
        RopeRenderer.positionCount = percision;
    }
    private void OnDisable()
    {
        isGrappling = false;
        RopeRenderer.enabled = false;
    }
    private void Update()
    {
        MoveTimer += Time.deltaTime;

        if (drawLine)
        {
            DrawRope();
        }
    }

    private void DrawRope()
    {
        if (!straightLine)
        {
            if (RopeRenderer.GetPosition(percision - 1).x != grapplingGun.grapplePoint.x)
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
                grapplingGun.Grapple();
                isGrappling = true;
                print("Do");
            }
            if (WavesCount > 0)
            {
                WavesCount -= Time.deltaTime * straightenLineSpeed;
                DrawWavesRope();
            }
            else
            {
                WavesCount = 0;
                DrawStraightRope();
            }
        }
    }
    private void DrawWavesRope()
    {
        for (int i = 0; i < percision; i++)
        {
            float delta = i / (percision - 1f);
            Vector2 offset = Vector2.Perpendicular(grapplingGun.DistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * WavesCount;
            Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeLaunchSpeedCurve.Evaluate(MoveTimer) * ropeLaunchSpeedMultiplayer);

            RopeRenderer.SetPosition(i, currentPosition);
        }
    }
    private void DrawStraightRope()
    {
        RopeRenderer.positionCount = 2;
        RopeRenderer.SetPosition(0, grapplingGun.grapplePoint);
        RopeRenderer.SetPosition(1, grapplingGun.firePoint.position);
    }
}
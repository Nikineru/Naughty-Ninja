using System;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private event Action Grabbed;
    [SerializeField] private event Action UnGrabbed;

    [Header("Scripts:")]
    public Rope grappleRope;

    [Header("Layer Settings:")]
    [SerializeField] private LayerMask invisible_layers;
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 6;

    [Header("Transform Refrences:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 80)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = true;
    [SerializeField] private float maxDistance = 4;

    [Header("Launching")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType Launch_Type = LaunchType.Transform_Launch;
    [Range(0, 5)] [SerializeField] private float launchSpeed = 5;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoCongifureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequency = 3;

    [SerializeField] private float cool_down;

    [Header("Grap Info")]
    public SpriteRenderer GrappedRenderer;

    private Vector2 MousePosition
    {
        get
        {
            Vector3 position = _Input.Hook.MousePosition.ReadValue<Vector2>();

            position.z = 20;
            position = _Camera.ScreenToWorldPoint(position);
            position.z = 0;

            return position;
        }
    }

    private Input _Input;
    private Camera _Camera;
    private bool IsHold;
    private float curret_cool_down;
    private LayerMask check_layers;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch,
    }

    [Header("Component Refrences:")]
    public SpringJoint2D m_springJoint2D;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 DistanceVector;
    public Vector2 Mouse_FirePoint_DistanceVector { get; private set; }

    public Rigidbody2D ballRigidbody;
    private float gravity_scale;


    private void Awake()
    {
        grappleRope.StopDraw();
        m_springJoint2D.enabled = false;
        check_layers = Physics.AllLayers - invisible_layers;
        //gravity_scale = ballRigidbody.gravityScale;

        _Camera = Camera.main;

        _Input = new Input();

        _Input.Hook.Use.performed += context =>
        {
            StartGrapple();
        };
        _Input.Hook.Use.canceled += context =>
        {
            IsHold = false;

            grappleRope.StopDraw();
            m_springJoint2D.enabled = false;
            //ballRigidbody.gravityScale = gravity_scale;
        };
    }

    private void OnEnable()
    {
        _Input.Enable();
    }
    private void OnDisable()
    {
        _Input.Disable();
    }
    private void Update()
    {
        Mouse_FirePoint_DistanceVector = (Vector3)MousePosition - gunPivot.position;
        
        if (IsHold)
        {
            if (grappleRope.IsDrawing)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                RotateGun(MousePosition, false);
            }

            if (launchToPoint && grappleRope.IsGrappling)
            {
                if (Launch_Type == LaunchType.Transform_Launch)
                {
                    gunHolder.position = Vector3.Lerp(gunHolder.position, grapplePoint, Time.deltaTime * launchSpeed);
                }
            }
        }
        else
        {
            RotateGun(MousePosition, true);
        }

        curret_cool_down += Time.deltaTime;
    }


    private void StartGrapple() 
    {
        if (curret_cool_down < cool_down)
            return;

        if (Mouse_FirePoint_DistanceVector.normalized.x <= 0)
            return;

        IsHold = true;
        curret_cool_down = 0;

        SetGrapplePoint();
    }
    private void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            Quaternion startRotation = gunPivot.rotation;
            gunPivot.rotation = Quaternion.Lerp(startRotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
    private void SetGrapplePoint()
    {
        RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, Mouse_FirePoint_DistanceVector.normalized, Mathf.Infinity, check_layers);

        if (_hit.transform == null)
            return;

        if ((_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll) && ((Vector2.Distance(_hit.point, firePoint.position) <= maxDistance) || !hasMaxDistance))
        {
            Grabbed?.Invoke();

            grapplePoint = _hit.point;
            GrappedRenderer = _hit.transform.GetComponent<SpriteRenderer>();
            DistanceVector = grapplePoint - (Vector2)gunPivot.position;
            grappleRope.StartGrapp(firePoint, _hit);
        }
    }
    public void Grapple()
    {
        if (!launchToPoint && !autoCongifureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequency;
        }

        if (!launchToPoint)
        {
            if (autoCongifureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }
            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            if (Launch_Type == LaunchType.Transform_Launch)
            {
                //gravity_scale = ballRigidbody.gravityScale;
                //ballRigidbody.gravityScale = 0;
                ballRigidbody.velocity = Vector2.zero;
            }
            if (Launch_Type == LaunchType.Physics_Launch)
            {
                m_springJoint2D.connectedAnchor = grapplePoint;
                m_springJoint2D.distance = 0;
                m_springJoint2D.frequency = launchSpeed;
                m_springJoint2D.enabled = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }
}
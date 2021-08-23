using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D), typeof(RopeRenderer))]
public class Hook : MonoBehaviour
{
    [SerializeField] private LayerMask HookMask;
    [SerializeField] private float MaxHookDistanse;

    private Camera _Camera;
    private HookInput _Input;
    private DistanceJoint2D RopeJoint;
    private RopeRenderer RopeRenderer;

    private RaycastHit2D RayCastHit;
    private Vector3 HookTarget;

    private void Awake()
    {
        _Camera = Camera.main;

        RopeRenderer = GetComponent<RopeRenderer>();

        RopeJoint = GetComponent<DistanceJoint2D>();
        RopeJoint.enabled = false;

        _Input = new HookInput();

        _Input.Hook.Use.performed += context => StartClimb();
        _Input.Hook.Use.canceled += context => StopClimb();
    }
    private void OnEnable()
    {
        _Input.Enable();
    }
    private void OnDisable()
    {
        _Input.Disable();
    }

    private void StartClimb() 
    {
        HookTarget = _Camera.ScreenToWorldPoint(Input.mousePosition);
        HookTarget.z = 0;

        RayCastHit = Physics2D.Raycast(transform.position, HookTarget - transform.position, MaxHookDistanse, HookMask);

        if (RayCastHit.collider != null)
        {
            if (RayCastHit.transform.TryGetComponent(out Rigidbody2D target_rigidbody))
            {
                RopeJoint.enabled = true;
                RopeJoint.connectedBody = target_rigidbody;
                RopeJoint.connectedAnchor = RayCastHit.transform.InverseTransformPoint(RayCastHit.point);

                RopeJoint.distance = Vector2.Distance(transform.position, RayCastHit.point);

                RopeRenderer.enabled = true;
            }
        }
    }
    private void StopClimb() 
    {
        RopeJoint.enabled = false;
        RopeRenderer.enabled = false;
    }
}
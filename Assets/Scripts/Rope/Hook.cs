using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private HingeJoint2D User;
    [SerializeField] private RopeBridge Rope;

    [SerializeField] private LayerMask DetectMask;
    [SerializeField] private float GrabDistanse;

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

    private void Awake()
    {
        _Camera = Camera.main;

        _Input = new Input();

        _Input.Hook.Use.performed += context => Grab();
        _Input.Hook.Use.canceled += context => StopGrab();
    }

    private void OnEnable()
    {
        _Input.Enable();
    }
    private void OnDisable()
    {
        _Input.Disable();
    }

    private void Grab()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, MousePosition, GrabDistanse, DetectMask);


        if(hit.transform != null)
        {
            User.enabled = true;
            Vector3 target_position = hit.point;
            Rope.Generate(target_position, transform.position, User);
        }
    }

    private void StopGrab() 
    {
        User.enabled = false;
        Rope.Clear();
    }
}

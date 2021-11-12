using UnityEngine;

[RequireComponent(typeof(SurfaceSlider), typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private JumpFX _jumper;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 velocity;

    private SurfaceSlider _surfaceSlider;
    private Rigidbody2D _rigidbody;
    private float _curret_speed;
    private Input _input;

    private Vector3 velocity_buffer;
    private Vector3 previous_position;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _surfaceSlider = GetComponent<SurfaceSlider>();

        _input = new Input();
        _input.Player.Jump.performed += context => _jumper.Jump();
    }
    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();
    private void Update()
    {
        var fwdDotProduct = Vector3.Dot(transform.forward, velocity_buffer);
        var upDotProduct = Vector3.Dot(transform.up, velocity_buffer);
        var rightDotProduct = Vector3.Dot(transform.right, velocity_buffer);

        velocity = new Vector3(rightDotProduct, upDotProduct, fwdDotProduct);
    }
    private void FixedUpdate()
    {
        Move(Vector2.right);

        velocity_buffer = (transform.position - previous_position) / Time.deltaTime;
        previous_position = transform.position;
    }

    public void StartMove() => _curret_speed = _speed;
    private void Move(Vector2 move_direction) 
    {
        if (_surfaceSlider.IsOnSurface == false)
            return;

        Vector2 along_surface_direction = _surfaceSlider.Project(move_direction.normalized);
        Vector2 offest = along_surface_direction * _curret_speed * Time.deltaTime;

        _rigidbody.AddForce(Vector2.right * _curret_speed);
    }
}
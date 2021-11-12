using UnityEngine;

[RequireComponent(typeof(SurfaceSlider), typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private JumpFX _jumper;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Tackle _tackle;

    private SurfaceSlider _surfaceSlider;
    private Rigidbody2D _rigidbody;
    private float _current_speed;
    private Input _input;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _surfaceSlider = GetComponent<SurfaceSlider>();

        _input = new Input();
        _input.Player.Jump.performed += context => _jumper.Jump();
        _input.Player.Tackle.performed += context => _tackle.RotatePlayer();
        _input.Player.Tackle.canceled += context => _tackle.RotatePlayer();
    }
    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();
    private void FixedUpdate()
    {
        Move(Vector2.right);
    }

    public void StartMove() => _current_speed = _speed;
    private void Move(Vector2 move_direction) 
    {
        if (_surfaceSlider.IsOnSurface == false)
            return;

        Vector2 along_surface_direction = _surfaceSlider.Project(move_direction.normalized);
        Vector2 offest = along_surface_direction * _current_speed * Time.deltaTime;

        _rigidbody.AddForce(_surfaceSlider.Project(move_direction.normalized) * _current_speed);
    }
}
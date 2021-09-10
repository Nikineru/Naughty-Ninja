using UnityEngine;

[RequireComponent(typeof(SurfaceSlider), typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PhisicsJump _jumper;
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private SurfaceSlider _surfaceSlider;
    private Input _input;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _surfaceSlider = GetComponent<SurfaceSlider>();

        _input = new Input();
        _input.Player.Jump.performed += context => _jumper.Jump(Vector2.right);
    }
    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();

    private void FixedUpdate()
    {
        Move(Vector2.right);
    }

    private void Move(Vector2 move_direction) 
    {
        if (_surfaceSlider.IsOnSurface == false)
            return;

        Vector2 along_surface_direction = _surfaceSlider.Project(move_direction.normalized);
        Vector2 offest = along_surface_direction * _speed * Time.deltaTime;

        _rigidbody.MovePosition(_rigidbody.position + offest);
    }
}
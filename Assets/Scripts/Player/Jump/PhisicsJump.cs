using UnityEngine;

public class PhisicsJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SurfaceSlider _surfaceSlider;
    [SerializeField] private JumpFX _fx;
    [SerializeField] private float _lenght;
    [SerializeField] private float _duration;

    [SerializeField] private float _jump_check_radius;
    [SerializeField] private LayerMask _ground_layer;
    [SerializeField] private Transform _jump_checker;

    private PureAnimation _playTime;

    private void Awake()
    {
        _playTime = new PureAnimation(this);
    }

    public void Jump(Vector2 direction) 
    {
        Collider2D hit = Physics2D.OverlapCircle(_jump_checker.position, _jump_check_radius, _ground_layer);

        if (hit == null)
            return;

        Vector3 target = transform.position + (Vector3)(direction * _lenght);
        Vector3 start_position = transform.position;
        PureAnimation fxPlayTime = _fx.PlayAnimation(transform, _duration);

        _playTime.Play(_duration, (progress) =>
        {
            _rigidbody.velocity = Vector2.zero;
            transform.position = Vector3.Lerp(start_position, target, progress) + (Vector3)fxPlayTime.LastChanges;
            return Vector3.zero;
        });
    }
}

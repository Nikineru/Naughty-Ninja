using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpFX : MonoBehaviour
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private GroundUnderLegs _GroundChecker;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _GroundChecker = GetComponent<GroundUnderLegs>();
    }

    public void Jump()
    {
        if (_GroundChecker.onGround)
        {
            print("jump!");
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * _rigidbody.gravityScale));
            _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}

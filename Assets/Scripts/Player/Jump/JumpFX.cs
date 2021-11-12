using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpFX : MonoBehaviour
{
    public float jumpAmount = 35;
    public float gravityScale = 10;
    public float fallingGravityScale = 40;
    [SerializeField] private float jumpHeight;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (_rigidbody.velocity.y >= 0)
        //{
        //    _rigidbody.gravityScale = gravityScale;
        //}
        //else if (_rigidbody.velocity.y < 0)
        //{
        //    _rigidbody.gravityScale = fallingGravityScale;
        //}
    }

    public void Jump() 
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * _rigidbody.gravityScale));
        _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}

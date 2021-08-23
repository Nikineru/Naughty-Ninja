using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float Speed =  1;
    private Rigidbody2D rigidbody;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float VelocityX = Input.GetAxis("Horizontal");

        if(Mathf.Abs(VelocityX) > 0)
            rigidbody.velocity = new Vector2(VelocityX * Speed, rigidbody.velocity.y);
    }
}

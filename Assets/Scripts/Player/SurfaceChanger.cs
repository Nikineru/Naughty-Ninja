using UnityEngine;

public class SurfaceChanger : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody.gravityScale = rigidbody.gravityScale * transform.position.ClampDirection(collision.transform.position).y;
        print(collision.transform.position.ClampDirection(transform.position).y);
    }
}

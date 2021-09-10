using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    public bool IsOnSurface { get; private set; }
    private Vector2 _normal;

    public Vector2 Project(Vector2 forward) 
    {
        return forward - Vector2.Dot(forward, _normal) * _normal;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _normal = collision.contacts[0].normal;
        IsOnSurface = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsOnSurface = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)_normal * 3);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)Project(transform.right) * 3);
    }
}
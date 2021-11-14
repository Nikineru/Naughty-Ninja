using UnityEngine;

public class ChunkHelper : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform left_gateway;
    [SerializeField] private Transform right_gateway;

    [Header("Properties")]
    [SerializeField] private float gateway_width = 2;
    [SerializeField] private float pointer_size = 0.5f;
    
    private readonly Vector3 camera_offest = new Vector3(0, 4.2f, 0);

    private void OnDrawGizmos()
    {
        if (_camera == null)
            return;

        Bounds camera_bounds = _camera.WorldBounds();
        Vector2 gateway_size = new Vector2(gateway_width, camera_bounds.size.y);


        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(camera_bounds.center, camera_bounds.size);

        Gizmos.color = Color.red;

        if (left_gateway != null) 
        {
            Gizmos.DrawWireCube(left_gateway.position + (Vector3)camera_offest, gateway_size);
            Gizmos.DrawSphere(left_gateway.position, pointer_size);
        }

        if(right_gateway != null) 
        {
            Gizmos.DrawWireCube(right_gateway.position + (Vector3)camera_offest, gateway_size);
            Gizmos.DrawSphere(right_gateway.position, pointer_size);
        }
    }
}
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        if (target == null)
            return;

        Vector2 Offest = Vector2.Lerp(transform.position, target.position, Time.deltaTime);
        transform.position = new Vector3(target.position.x, Offest.y, transform.position.z);
    }
}

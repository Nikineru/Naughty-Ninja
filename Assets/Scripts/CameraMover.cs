using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offest;

    private void Update()
    {
        if (target == null)
            return;

        Vector2 shift = Vector2.Lerp(transform.position - offest, target.position, Time.deltaTime);
        transform.position = new Vector3(target.position.x, shift.y, transform.position.z);
        transform.position += offest;
    }
}

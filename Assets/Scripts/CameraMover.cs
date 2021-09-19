using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offest;

    private void Update()
    {
        if (target == null)
            return;

        Vector2 shift = Vector2.Lerp(transform.position, target.position, Time.deltaTime);
        transform.position = new Vector3(target.position.x + offest.x, shift.y + offest.y, transform.position.z);
    }
}

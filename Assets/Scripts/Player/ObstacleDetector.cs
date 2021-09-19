using UnityEngine;
using UnityEngine.Events;

public class ObstacleDetector : MonoBehaviour
{
    public UnityEvent TouchedObstacle { get => touched_obstacle; }
    [SerializeField] private UnityEvent touched_obstacle;

    private enum DetectTypes 
    {
        Trigger,
        Collision,
        Both
    };
    [SerializeField] private DetectTypes detect_type;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DetectObstacle(DetectTypes.Collision, collision.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DetectObstacle(DetectTypes.Trigger, collision.transform);
    }

    private void DetectObstacle(DetectTypes detection_type, Transform target) 
    {
        if (detection_type != detect_type && detect_type != DetectTypes.Both)
            return;

        if (target.TryGetComponent(out Obstacle obstacle))
            touched_obstacle?.Invoke();
    }
}

using UnityEngine;

public class ChildrenDestroier : MonoBehaviour
{
    private enum RandomizeMode
    {
        JustDestroy,
        ChangePosition,
        ChangeScale,
        ChangeBoth
    }

    [SerializeField] private int leave_count;
    [SerializeField] private RandomizeMode mode;

    [Header("Random offsets:")]
    [SerializeField] private Vector2 scale_offset;
    [SerializeField] private Vector2 position_offset;

    private void OnEnable()
    {
        while (transform.childCount > leave_count)
        {
            Transform destroy_child = transform.GetChild(Random.Range(0, transform.childCount));
            DestroyImmediate(destroy_child.gameObject);
        }

        foreach (Transform child in transform.GetComponentInChildren<Transform>())
        {
            Randomize(child);
        }
    }

    public void Initialize(int leave_count)
    {
        this.leave_count = leave_count;
    }

    private void Randomize(Transform target) 
    {
        switch (mode)
        {
            case RandomizeMode.JustDestroy:
                break;
            case RandomizeMode.ChangePosition:
                transform.position = transform.position.RandomOffset(position_offset);
                break;
            case RandomizeMode.ChangeScale:
                transform.localScale = transform.localScale.RandomOffset(scale_offset);
                break;
            case RandomizeMode.ChangeBoth:
                print($"WAS: {transform.position}");
                transform.position = transform.position.RandomOffset(position_offset);
                print($"NEW: {transform.position}");

                transform.localScale = transform.localScale.RandomOffset(scale_offset);
                break;
        }
    }
}

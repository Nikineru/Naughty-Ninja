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

    [Header("")]

    [SerializeField] private bool random_count = false;
    [SerializeField] private int min_count;
    [SerializeField] private int max_count;

    [Header("")]

    [SerializeField] private RandomizeMode mode;
    [SerializeField] private Vector2 scale_offset;
    [SerializeField] private Vector2 position_offset;

    private void OnEnable()
    {
        if (random_count)
            leave_count = Random.Range(min_count, max_count);

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

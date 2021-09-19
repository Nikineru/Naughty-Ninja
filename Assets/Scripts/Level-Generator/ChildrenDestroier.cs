using UnityEngine;

public class ChildrenDestroier : MonoBehaviour
{
    [SerializeField] private int leave_count;

    private void Awake()
    {
        while (transform.childCount > leave_count)
        {
            Transform destroy_child = transform.GetChild(Random.Range(0, transform.childCount));
            DestroyImmediate(destroy_child.gameObject);
        }
    }
}

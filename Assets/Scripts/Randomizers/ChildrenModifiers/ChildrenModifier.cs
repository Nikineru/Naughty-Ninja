using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ChildrenModifier : MonoBehaviour
{
    [SerializeField] private int unchanged_count;

    [Header("")]

    [SerializeField] private bool randomize_count;
    [SerializeField] private int min_count;
    [SerializeField] private int max_count;

    private void OnEnable()
    {
        ApplyModifications();
    }
    private void OnDisable()
    {
        DiscardModifications();
    }
    private void OnValidate()
    {
        if (unchanged_count < 0)
            unchanged_count = 0;

        else if (unchanged_count > transform.childCount)
            unchanged_count = transform.childCount;
    }

    public void ApplyModifications()
    {
        if (randomize_count)
            unchanged_count = Random.Range(min_count, max_count);

        List<Transform> children = transform.GetComponentsInChildren<Transform>().Where(i => i.parent == transform && i != transform).ToList();

        while (children.Count > unchanged_count)
        {
            Transform modify_child = children[Random.Range(0, children.Count)];

            Modify(modify_child.gameObject);
            children.Remove(modify_child);
        }
    }
    public void DiscardModifications() 
    {
        for (int child_i = 0; child_i < transform.childCount; child_i++)
        {
            Transform child = transform.GetChild(child_i);
            Discard(child.gameObject);
        }
    }

    protected abstract void Modify(GameObject child);
    protected virtual void Discard(GameObject child) { }
}
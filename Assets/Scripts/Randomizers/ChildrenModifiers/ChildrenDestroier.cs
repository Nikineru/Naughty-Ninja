using UnityEngine;

public class ChildrenDestroier : ChildrenModifier
{
    protected override void Modify(GameObject child)
    {
        DestroyImmediate(child);
    }
}
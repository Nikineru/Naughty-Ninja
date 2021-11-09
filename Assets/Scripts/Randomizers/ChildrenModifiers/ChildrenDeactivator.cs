using UnityEngine;

public class ChildrenDeactivator : ChildrenModifier
{
    protected override void Modify(GameObject child)
    {
        child.SetActive(false);
    }

    protected override void Discard(GameObject child)
    {
        child.SetActive(true);
    }
}

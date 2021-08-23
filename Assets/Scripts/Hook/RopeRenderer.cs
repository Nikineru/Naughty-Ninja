using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeRenderer : MonoBehaviour
{
    private Transform Origin;
    private LineRenderer _Renderer;

    private void Awake()
    {
        _Renderer = GetComponent<LineRenderer>();
        enabled = false;
    }
    public void SetOrigin(Transform origin)
    {
        Origin = origin;
    }
    public void SetTarget(Vector2 target) 
    {
        _Renderer.SetPosition(1, target);
    }

    private void Update()
    {
        _Renderer.SetPosition(0, Origin.position);
    }
}
using System.Collections;
using UnityEngine;

public class JumpFX : MonoBehaviour
{
    [SerializeField] private AnimationCurve _heightCurve;
    [SerializeField] private float _height;
    [SerializeField] private PureAnimation _playtime;
    [SerializeField] private SurfaceSlider surface_slider;

    private void Awake()
    {
        _playtime = new PureAnimation(this);
    }

    public PureAnimation PlayAnimation(Transform jumper, float duration) 
    {
        Vector2 start_position = transform.position;

        _playtime.Play(duration, (float progress) =>
        {
            Vector2 animation_value = new Vector2(0, surface_slider._normal.y * _height * _heightCurve.Evaluate(progress));
            Vector2 position = Vector2.Scale(animation_value, jumper.up);
            return position;
        });

        return _playtime;
    }
}

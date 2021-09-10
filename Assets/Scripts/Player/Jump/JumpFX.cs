using System.Collections;
using UnityEngine;

public class JumpFX : MonoBehaviour
{
    [SerializeField] private AnimationCurve _heightCurve;
    [SerializeField] private float _height;
    [SerializeField] private PureAnimation _playtime;

    private void Awake()
    {
        _playtime = new PureAnimation(this);
    }

    public PureAnimation PlayAnimation(Transform jumper, float duration) 
    {
        _playtime.Play(duration, (float progress) =>
        {
            Vector2 position = Vector2.Scale(new Vector2(0, _height * _heightCurve.Evaluate(progress)), jumper.up);
            return position;
        });

        return _playtime;
    }
}

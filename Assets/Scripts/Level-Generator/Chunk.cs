using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Transform Begin { get => _begin; }
    [SerializeField] private Transform _begin;

    public Transform End { get => _end; }
    [SerializeField] private Transform _end;

    public AnimationCurve ChanceByDistanse { get => _ChanceByDistanse; }
    [SerializeField] private AnimationCurve _ChanceByDistanse;
}

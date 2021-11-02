using UnityEngine;

public class Money : MonoBehaviour
{
    public byte amount;

    public float Height { get => height; }
    [SerializeField] private float height;

    [SerializeField] private Transform TopCoin;
    [SerializeField] private Transform BottomCoin;

    private void OnValidate()
    {
        if (TopCoin == null || BottomCoin == null)
            return;

        height = (TopCoin.localPosition.y - BottomCoin.localPosition.y) + 1 * TopCoin.localScale.y;
    }
}

using TMPro;
using UnityEngine;

public class MoneyPicker : MonoBehaviour
{
    public int money_amount { get; private set; }
    [SerializeField] private TextMeshProUGUI money_output;

    public void OnTriggerEnter2D(Collider2D  other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            money_amount += coin.Amount;
            other.gameObject.SetActive(false);
            money_output.text = $"Coins:\n{money_amount}";
        }
    }
}

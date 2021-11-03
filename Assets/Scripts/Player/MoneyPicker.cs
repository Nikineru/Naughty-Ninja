using UnityEngine;

public class MoneyPicker : MonoBehaviour
{
    public int money_amount { get; private set; } 

    public void OnTriggerEnter2D(Collider2D  other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            money_amount += coin.Amount;
            Destroy(other.gameObject);
        }
    }
}

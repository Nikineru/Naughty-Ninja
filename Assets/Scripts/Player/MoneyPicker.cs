using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPicker : MonoBehaviour
{
    private string moneyTag = "Money";
    public uint moneyAmount; 
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D  other)
    {
        if (other.CompareTag(moneyTag))
        {
            moneyAmount += other.gameObject.GetComponent<Money>().amount;
            Destroy(other.gameObject);
        }
    }
}

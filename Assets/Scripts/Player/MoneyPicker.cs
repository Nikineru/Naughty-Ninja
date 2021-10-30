using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPicker : MonoBehaviour
{
    private string moneyTag = "Money";
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D  other)
    {
        if (other.CompareTag(moneyTag))
            Destroy(other.gameObject);
    }
}

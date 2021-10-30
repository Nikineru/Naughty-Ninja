using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGenerator : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;

    public Transform moneyPrefab;

    private void Start()
    {
        float position_X = Random.Range(pos1.position.x, pos2.position.x);
        float position_Y = Random.Range(pos2.position.y, pos1.position.y);

        Vector2 MoneyPos = new Vector2(position_X, position_Y);

        Transform Cash = Instantiate(moneyPrefab, MoneyPos, Quaternion.identity);
        Cash.parent = transform;
    }
}

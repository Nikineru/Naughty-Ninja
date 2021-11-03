using UnityEngine;
using System.Linq;

public class CoinsPattern : ChildrenDestroier
{    
    public (float Bottom, float Top) GetHorizontalBordersShift()
    {
        Transform[] coins = GetComponentsInChildren<Coin>().Select(i => i.transform).ToArray();

        float top_shift = GetCoinShifts(coins[0]).Top;
        float bottom_shift = GetCoinShifts(coins[0]).Bottom;

        foreach (Transform coin in coins)
        {
            var shifts = GetCoinShifts(coin);

            if (shifts.Bottom < bottom_shift) 
                bottom_shift = shifts.Bottom;

            else if (shifts.Top > top_shift) 
                top_shift = shifts.Top;
        }

        return (Mathf.Abs(bottom_shift), Mathf.Abs(top_shift));
    }

    public (float Bottom, float Top) GetCoinShifts(Transform coin) 
    {
        float coin_height = coin.position.y;
        float coin_size = coin.localScale.y / 2;


        return (coin_height - coin_size, coin_height + coin_size);
    }
}
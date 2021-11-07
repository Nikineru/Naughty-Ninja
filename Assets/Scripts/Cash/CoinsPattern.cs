using UnityEngine;
using System.Linq;

public class CoinsPattern : ChildrenDestroier
{
    public (float Bottom, float Top, float Right, float Left) GetBordersShift()
    {
        Transform[] coins = GetComponentsInChildren<Coin>().Select(i => i.transform).ToArray();

        float top_shift = GetCoinShifts(coins[0]).Top;
        float bottom_shift = GetCoinShifts(coins[0]).Bottom;
        float right_shift = GetCoinShifts(coins[0]).Right;
        float left_shift = GetCoinShifts(coins[0]).Left;


        foreach (Transform coin in coins)
        {
            var shifts = GetCoinShifts(coin);

            if (shifts.Bottom < bottom_shift) 
                bottom_shift = shifts.Bottom;

            else if (shifts.Top > top_shift) 
                top_shift = shifts.Top;

            if (shifts.Right > right_shift)
                right_shift = shifts.Right;

            else if (shifts.Left < left_shift)
                left_shift = shifts.Left;
        }

        return (Mathf.Abs(bottom_shift), Mathf.Abs(top_shift), Mathf.Abs(right_shift), Mathf.Abs(left_shift));
    }

    public (float Bottom, float Top, float Right, float Left) GetCoinShifts(Transform coin) 
    {
        float coin_height = coin.localScale.y / 2;
        float coin_width = coin.localScale.x / 2;

        float position_x = coin.localPosition.x;
        float position_y = coin.localPosition.y;


        return (position_y - coin_height, position_y + coin_height, position_x + coin_width, position_x - coin_width);
    }
}
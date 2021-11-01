using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI score_output;

    public float Score
    {
        get 
        {
            return score;   
        }
        set 
        {
            score = value;
            score_output.text = $"Score {System.Math.Round(score, 2)}";
        }
    }
    private float score;
    private float last_player_x = 0;

    private void Update()
    {
        float player_x = player.transform.position.x;
        float shift = player_x - last_player_x;

        Score += shift > 0 ? shift : 0;
        last_player_x = player_x;
    }
}

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
            score_output.text = $"Score:\n{System.Math.Round(score, 2)}";
        }
    }
    private float score;

    private float max_player_shift;

    private void Awake()
    {
        max_player_shift = player.transform.position.x;
    }

    private void Update()
    {
        float player_x = player.transform.position.x;
        float shift = player_x - max_player_shift;

        if (player_x > max_player_shift) 
        {
            Score += shift > 0 ? shift : 0;
            max_player_shift = player_x;
        }
    }
}

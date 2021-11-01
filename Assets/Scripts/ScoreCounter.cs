using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            score_output.text = $"Score: {score}";
        }
    }
    private float score;

    private void Update()
    {
        float player_shift = (float)System.Math.Round(player.transform.position.x, 2);

        Score =  player_shift > Score ? player_shift : Score;
    }
}

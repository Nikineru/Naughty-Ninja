using UnityEngine;

public class RandomDestroier : MonoBehaviour
{
    [SerializeField] private float staying_chance;

    private void Awake()
    {
        if (Random.value > staying_chance) Destroy(gameObject);
    }
}

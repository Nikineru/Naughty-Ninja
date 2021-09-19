using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite_renderer;
    [SerializeField] private PlayerMover mover;
    [SerializeField] private Hook hook;

    public void Die() 
    {
        sprite_renderer.color = Color.red;
        mover.enabled = false;
        hook.enabled = false;
    }
}

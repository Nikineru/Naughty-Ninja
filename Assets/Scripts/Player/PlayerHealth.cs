using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private UnityEvent Died;

    [SerializeField] private SpriteRenderer sprite_renderer;
    [SerializeField] private PlayerMover mover;
    [SerializeField] private SurfaceChanger surface_changer;
    [SerializeField] private Hook hook;

    public void Die() 
    {
        sprite_renderer.color = Color.red;
        mover.enabled = false;
        hook.enabled = false;
        surface_changer.enabled = false;

        Died?.Invoke();
    }
}

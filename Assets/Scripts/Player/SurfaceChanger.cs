using UnityEngine;

public class SurfaceChanger : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SurfaceSlider slider;
    private bool orientation = true;
    private Input _Input;


    private void Awake()
    {
        _Input = new Input();

        _Input.Player.Jump.performed += context =>
        {
            orientation = !orientation;
            ChangeOrientation(orientation);
        };
    }

    private void OnEnable()
    {
        _Input.Enable();
    }
    private void OnDisable()
    {
        _Input.Disable();
    }

    private void ChangeOrientation(bool orientation)
    {
        if (slider.IsOnSurface == false)
            return; 

        float curret_gravity = Mathf.Abs(_rigidbody.gravityScale);
        float scale_y = Mathf.Abs(transform.localScale.y);

        _rigidbody.gravityScale = orientation ? curret_gravity : -curret_gravity;
        transform.localScale = new Vector2(transform.localScale.x, orientation ? scale_y : -scale_y);
    }
}
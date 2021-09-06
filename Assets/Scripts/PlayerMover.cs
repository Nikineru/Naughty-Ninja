using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForse;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform JumpCollider;
    [SerializeField] private float JumpCheckRadius;

    private Input _Input;
    private Rigidbody2D _Rigidbody;

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();

        _Input = new Input();
        _Input.Player.Jump.performed += context => Jump();
    }
    private void OnEnable()
    {
        _Input.Enable();
    }
    private void OnDisable()
    {
        _Input.Disable();
    }

    private void FixedUpdate()
    {
        _Rigidbody.velocity = new Vector2(Speed, _Rigidbody.velocity.y);
    }

    private void Jump()
    {
        Collider2D hit = Physics2D.OverlapCircle(JumpCollider.position, JumpCheckRadius, GroundLayer);


        if (hit != null && hit.transform != null)
        {
            _Rigidbody.AddForce(Vector2.up * JumpForse);
            print("Jump");
        }
    }
}

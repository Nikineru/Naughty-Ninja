using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class JumpV01 : MonoBehaviour
{
    private Rigidbody2D _rig;
    [SerializeField] private float Force;
    [SerializeField] private GroundUnderLegs _GroundChecker;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _GroundChecker = GetComponent<GroundUnderLegs>();
    }
    public void Jump(float speedCoff)
    {
        print(speedCoff);
        if (_GroundChecker.onGround)
        {
            _rig.AddForce(Vector2.up * Force);
            _GroundChecker.onGround = false;
        }
    }
}

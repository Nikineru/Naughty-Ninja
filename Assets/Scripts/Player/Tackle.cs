using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackle : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GroundUnderLegs _GroundChecker;
    private CapsuleCollider2D _capsuleStay;
    [SerializeField] private Transform GroundChecker;
    [SerializeField] private Transform playerPivot;
    private bool IsTackling;

    private void Awake()
    {
        player = GetComponent<Transform>();
        _capsuleStay = GetComponent<CapsuleCollider2D>();
    }

    public void RotatePlayer()
    {
        if (_GroundChecker.onGround && !IsTackling)
        {
            player.position = new Vector2(player.position.x, player.position.y + 5f);
            GroundChecker.rotation = Quaternion.Euler(0, 0, -90);
            playerPivot.rotation = Quaternion.Euler(0, 0, 90);
            IsTackling = true;
        }
        else if(IsTackling)
        {
            player.position = new Vector2(player.position.x, player.position.y + 5f);
            IsTackling = false;
            playerPivot.rotation = Quaternion.Euler(0, 0, 0);
            GroundChecker.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}

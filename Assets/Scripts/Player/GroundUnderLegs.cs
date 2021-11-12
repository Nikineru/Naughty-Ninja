using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUnderLegs : MonoBehaviour
{
    public bool onGround;
    private void OnTriggerStay2D(Collider2D collision)
    {
        onGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onGround = false;
    }
}

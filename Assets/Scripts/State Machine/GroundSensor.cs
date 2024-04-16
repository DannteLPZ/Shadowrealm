using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;

    private bool _isGrounded;
    public bool IsGrounded => _isGrounded;

    private void FixedUpdate()
    {
        CheckGrounded();
    }
    private void CheckGrounded()
    {
        _isGrounded = Physics2D.OverlapAreaAll(_groundCheck.bounds.min, _groundCheck.bounds.max, _whatIsGround).Length > 0;
    }
}

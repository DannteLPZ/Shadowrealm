using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;

    private bool _isGrounded;
    public bool IsGrounded => _isGrounded;

    private string _groundType;
    public string GroundType => _groundType;

    private void FixedUpdate()
    {
        CheckGrounded();
    }
    private void CheckGrounded()
    {
        Collider2D[] grounds = Physics2D.OverlapAreaAll(_groundCheck.bounds.min, _groundCheck.bounds.max, _whatIsGround);
        _isGrounded = grounds.Length > 0;
        _groundType = _isGrounded ? grounds[0].tag : string.Empty;
    }
}

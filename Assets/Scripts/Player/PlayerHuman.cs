using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHuman : HumanMotor
{
    [SerializeField] private GroundCheck groundCheck;

    private float _jumpForce;
    private void Start()
    {
        _jumpForce = 20f;
    }
    override protected void Jump()
    {
        if (!groundCheck.isGrounded)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}

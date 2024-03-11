using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private Vector2 _movement;
    private float _groundCheckRadius = 0.3f;
    private bool _isGrounded;
    private bool _facingRight = true;

    [Header("Components")]
    [SerializeField] private Transform _groundCheck;
    private Rigidbody2D _rb;

    [Header("Layers")]
    [SerializeField] private LayerMask _groundLayer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        IsGrounded();
        Jump();
    }

    private void FixedUpdate()
    {
        float horizontalVelocity = _movement.normalized.x * _speed;
        _rb.velocity = new Vector2(horizontalVelocity, _rb.velocity.y);
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _movement = new Vector2(horizontalInput, 0f);

        if (horizontalInput < 0 && _facingRight)
        {
            Flip();
        }
        else if (horizontalInput > 0 && !_facingRight)
        {
            Flip();
        }
    }

    private bool IsGrounded()
    {
        return _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    private void Jump()
    {
        if (!_isGrounded) return;
        if (Input.GetButtonDown("Jump") && _isGrounded)
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}

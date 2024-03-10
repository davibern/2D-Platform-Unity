using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private Vector2 _movement;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private float _groundCheckRadius = 0.3f;
    private bool _isGrounded;

    [Header("GameObjects")]
    private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheck;

    [Header("Layers")]
    [SerializeField] private LayerMask _groundLayer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
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

        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    private void Jump()
    {
        if (!_isGrounded) return;
        if (Input.GetButtonDown("Jump") && _isGrounded)
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}

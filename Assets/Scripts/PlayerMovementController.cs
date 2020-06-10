using System;
using UnityEngine;

[RequireComponent(typeof(CharacterGrounding), typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour, IMove
{
    [SerializeField] private int _moveSpeed = 5;
    [SerializeField] private int _jumpForce = 300;
    [SerializeField] private int _wallJumpForce = 300;
    [SerializeField] private int _bounceForce = 150;
    
    private Rigidbody2D _rigidbody2D;
    private CharacterGrounding _characterGrounding;

    public float Speed { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterGrounding = GetComponent<CharacterGrounding>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && _characterGrounding.IsGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);

            if (_characterGrounding.GroundedDirection != Vector2.down)
            {
                _rigidbody2D.AddForce(_characterGrounding.GroundedDirection * -1f * _wallJumpForce);
            }
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Speed = horizontal;

        Vector3 movement = new Vector3(horizontal, 0);

        transform.position += movement * _moveSpeed * Time.deltaTime;
    }

    public void Bounce()
    {
        _rigidbody2D.AddForce(Vector2.up * _bounceForce);
    }
}

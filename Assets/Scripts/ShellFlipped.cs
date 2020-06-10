using System;
using UnityEngine;

public class ShellFlipped : MonoBehaviour
{
    [SerializeField] private float _shellSpeed = 5f;

    private Collider2D _collider;
    private Rigidbody2D _rigidBody2D;

    private Vector2 direction;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidBody2D.velocity = new Vector2(direction.x * _shellSpeed, _rigidBody2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.WasHitByPlayer())
        {
            HandlePlayerCollision(collision);
        }
        else
        {
            if (collision.WasSide())
            {
                LaunchShell(collision);
                var takeShellHits = collision.collider.GetComponent<ITakeShellHits>();
                if (takeShellHits != null)
                {
                    takeShellHits.HandleShellHit(this);
                }
            }
        }
    }

    private void HandlePlayerCollision(Collision2D collision)
    {
        var playerMovementController = collision.collider.GetComponent<PlayerMovementController>();

        if (direction.magnitude == 0)
        {
            LaunchShell(collision);
            
            if (collision.WasTop())
                playerMovementController.Bounce();
        }
        else
        {
            if (collision.WasTop())
            {
                direction = Vector2.zero;
                playerMovementController.Bounce();
            }
            else
            {
                GameManager.Instance.KillPlayer();
            }
        }
    }

    private void LaunchShell(Collision2D collision)
    {
        if (collision.WasLeft())
            direction = Vector2.right;
        else if (collision.WasRight())
            direction = Vector2.left;
    }
}

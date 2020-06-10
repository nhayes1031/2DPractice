using System;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject spawnOnStompPrefab;
    
    private Collider2D _collider;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _direction = Vector2.left;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direction * speed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        if (ReachedEdge() || HitNonPlayer())
            SwitchDirections();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.WasHitByPlayer())
        {
            if (collision.WasTop())
                HandleWalkerStomped(collision.collider.GetComponent<PlayerMovementController>());
            else
                GameManager.Instance.KillPlayer();
        }
    }

    private void HandleWalkerStomped(PlayerMovementController playerMovementController)
    {
        if (spawnOnStompPrefab != null)
            Instantiate(spawnOnStompPrefab, transform.position, transform.rotation);

        playerMovementController.Bounce();

        Destroy(gameObject);
    }

    private bool HitNonPlayer()
    {
        float x = GetForwardX();
        float y = transform.position.y;
        Vector2 origin = new Vector2(x, y);

        Debug.DrawRay(origin, _direction * 0.1f);
        var hit = Physics2D.Raycast(origin, _direction, 0.1f);

        if (hit.collider == null || 
            hit.collider.isTrigger ||
            hit.collider.GetComponent<PlayerMovementController>() != null)
            return false;

        return true;
    }

    private void SwitchDirections()
    {
        _direction *= -1;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    private bool ReachedEdge()
    {
        float x = GetForwardX();
        float y = _collider.bounds.min.y;
        Vector2 origin = new Vector2(x, y);

        Debug.DrawRay(origin, Vector2.down * 0.1f);
        var hit = Physics2D.Raycast(origin, Vector2.down, 0.1f);

        if (hit.collider == null)
            return true;

        return false;
    }

    private float GetForwardX()
    {
        return _direction.x == -1 ? _collider.bounds.min.x - 0.1f : _collider.bounds.max.x + 0.1f;
    }
}

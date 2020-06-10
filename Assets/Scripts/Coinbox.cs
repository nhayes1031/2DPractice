using System;
using UnityEngine;

public class Coinbox : MonoBehaviour, ITakeShellHits
{
    [SerializeField] private SpriteRenderer _enabledSprite;
    [SerializeField] private SpriteRenderer _disabledSprite;
    [SerializeField] private int _totalCoins = 1;
    private int _remainingCoins;
    private Animator _animator;

    private void Awake()
    {
        _remainingCoins = _totalCoins;
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_remainingCoins > 0
            && collision.WasHitByPlayer()
            && collision.WasBottom())
        {
            TakeCoin();
        }
    }

    private static bool WasHitByPlayer(Collision2D collision)
    {
        return collision.collider.GetComponent<PlayerMovementController>() != null;
    }

    private static bool WasHitFromBottomSide(Collision2D collision)
    {
        return collision.contacts[0].normal.y > 0.5;
    }

    public void HandleShellHit(ShellFlipped shellFlipped)
    {
        if (_remainingCoins > 0)
            TakeCoin();
    }

    private void TakeCoin()
    {
        GameManager.Instance.AddCoin();
        _remainingCoins--;
        _animator.SetTrigger("FlipCoin");

        if (_remainingCoins <= 0)
        {
            _enabledSprite.enabled = false;
            _disabledSprite.enabled = true;
        }
    }
}

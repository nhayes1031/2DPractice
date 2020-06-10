using UnityEngine;

public class BreakableBox : MonoBehaviour, ITakeShellHits
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.WasHitByPlayer()
            && collision.WasBottom())
        {
            Destroy(gameObject);
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
        Destroy(gameObject);
    }
}

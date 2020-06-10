using UnityEngine;

public class KillOnTouch : MonoBehaviour, ITakeShellHits
{
    public void HandleShellHit(ShellFlipped shellFlipped)
    {
        Destroy(shellFlipped.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerMovementController = collision.collider.GetComponent<PlayerMovementController>();
        if (playerMovementController != null)
        {
            GameManager.Instance.KillPlayer();
        }
    }
}

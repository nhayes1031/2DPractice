using UnityEngine;

public class UICoinImage : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        GameManager.Instance.OnCoinsChanged += Pulse;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCoinsChanged -= Pulse;
    }
    private void Pulse(int coins)
    {
        _animator.SetTrigger("Pulse");
    }
}

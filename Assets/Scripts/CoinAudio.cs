using UnityEngine;

public class CoinAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.Instance.OnCoinsChanged += HandleOnCoinsChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCoinsChanged -= HandleOnCoinsChanged;
    }

    private void HandleOnCoinsChanged(int coins)
    {
        _audioSource.Play();
    }
}

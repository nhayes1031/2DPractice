using TMPro;
using UnityEngine;

public class UICoinsText : MonoBehaviour
{
    private TextMeshProUGUI _tmproText;

    private void Awake()
    {
        _tmproText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnCoinsChanged += HandleOnCoinsChanged;
        _tmproText.text = GameManager.Instance.Coins.ToString();
    }

    private void HandleOnCoinsChanged(int coins)
    {
        _tmproText.text = coins.ToString();
    }
}
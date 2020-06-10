using TMPro;
using UnityEngine;

public class UILivesText : MonoBehaviour
{
    private TextMeshProUGUI _tmproText;

    private void Awake()
    {
        _tmproText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnLivesChanged += HandleOnLivesChanged;
    }

    private void HandleOnLivesChanged(int lives)
    {
        _tmproText.text = lives.ToString();
    }
}

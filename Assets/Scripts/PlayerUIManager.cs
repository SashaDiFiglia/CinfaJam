using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("PlayerComponents")]
    [SerializeField] private CharacterHealth _playerHealth;

    [SerializeField] private PlayerKeyInventory _playerKeyInventory;

    [Header("Sprites")]
    [SerializeField] private Sprite _fullHeart;

    [SerializeField] private Sprite _emptyHeart;

    [Header("UI")]
    [SerializeField] private List<Image> _hearts = new List<Image>();

    [SerializeField] private TextMeshProUGUI _keyNumber;

    private void Awake()
    {
        _playerHealth.OnHealthChange += UpdateHealthUI;

        _playerKeyInventory.OnKeyAmountChange += UpdateKeyUI;
    }

    private void UpdateHealthUI(float hp)
    {
        var maxHealth = _playerHealth.MaxHealth;
        var currentHealth = _playerHealth.CurrentHealth;

        for (var i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                _hearts[i].sprite = _fullHeart;
                continue;
            }

            _hearts[i].sprite = _emptyHeart;
        }
    }

    private void UpdateKeyUI(int amount)
    {
        _keyNumber.text = amount.ToString();
    }
}
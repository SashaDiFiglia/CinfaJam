using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour {
	[Header("PlayerComponents")] [SerializeField]
	private CharacterHealth _playerHealth;

	[SerializeField] private PlayerKeyInventory _playerKeyInventory;

	[Header("UI")] [SerializeField] private List<Image> _hearts = new List<Image>();

	[SerializeField] private TextMeshProUGUI _keyNumber;

	private IEnumerator Start() {
		yield return null;
		_playerHealth = FindFirstObjectByType<CharacterHealth>();
		_playerKeyInventory = FindFirstObjectByType<PlayerKeyInventory>();
		yield return null;
		_playerHealth.OnHealthChange += UpdateHealthUI;
		_playerKeyInventory.OnKeyAmountChange += UpdateKeyUI;
	}

	private void OnDestroy() {
		_playerHealth.OnHealthChange -= UpdateHealthUI;
		_playerKeyInventory.OnKeyAmountChange -= UpdateKeyUI;
	}

	private void UpdateHealthUI(float hp) {
		var maxHealth = _playerHealth.MaxHealth;
		var currentHealth = _playerHealth.CurrentHealth;

		for (var i = 0; i < maxHealth; i++) {
			if (i < currentHealth) {
				_hearts[i].enabled = true;
				continue;
			}

			_hearts[i].enabled = false;
		}
	}

	private void UpdateKeyUI(int amount) {
		_keyNumber.text = amount.ToString();
	}
}
using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    public bool IsFirst;

    [SerializeField] private GameObject _light;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _inactiveSprite;

    private SpriteRenderer _renderer;

    public event Action<Checkpoint> OnPlayerEntered;

    private void Awake()
    {
        if (TryGetComponent<BoxCollider2D>(out var c))
        {
            c.isTrigger = true;
        }

        if (TryGetComponent<SpriteRenderer>(out var renderer))
        {
            _renderer = renderer;

            if (IsFirst)
            {
                _renderer.sprite = _activeSprite;
                _light.SetActive(true);
                return;
            }

            _renderer.sprite = _inactiveSprite;
            _light.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<CharacterInput>(out var character))
        {
            Debug.Log("entratp");

            OnPlayerEntered?.Invoke(this);
        }
    }

    public void SetActive(bool state)
    {
        if (state)
        {
            _renderer.sprite = _activeSprite;
            _light.SetActive(true);
        }
        else
        {
            _renderer.sprite = _inactiveSprite;
            _light.SetActive(false);
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;

        Handles.DrawWireCube(transform.position, Vector3.one * 0.5f);
    }

#endif
}
using System;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoBehaviour
{
    [Header("Opens With Key?")] [SerializeField]
    private bool _openWithKey;

    [SerializeField] private float _distanceCheck = 2f;


    [Header("Open Sprite")] [SerializeField]
    private Sprite _openSprite;

    private Collider2D _col;
    private SpriteRenderer _renderer;
    private PlayerKeyInventory _playerKeys;
    private bool _isOpen = false;


    private void Awake()
    {
        _col = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();

        if (_openWithKey)
        {
            _playerKeys = FindFirstObjectByType<PlayerKeyInventory>();
        }
    }


    private void Update()
    {
        if (_openWithKey)
        {
            var distance = Vector3.Distance(transform.position, _playerKeys.transform.position);
            if (distance <= _distanceCheck && _playerKeys.GetHoldKeys() > 0)
            {
                if (!_isOpen)
                {
                    Open();
                    _playerKeys.ReduceKey();
                    _isOpen = true;
                }
            }
        }
    }


    [Button]
    public void Open()
    {
        _renderer.sprite = _openSprite;
        _col.enabled = false;
    }
}
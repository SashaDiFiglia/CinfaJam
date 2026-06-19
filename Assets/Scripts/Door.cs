using System;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoBehaviour
{
    [Header ("Opens With Key?")]
    [SerializeField] private bool _openWithKey;
    [SerializeField] private float _distanceCheck = 0.6f;

    
    [Header("Open Sprite")]
    [SerializeField] private Sprite _openSprite;
    
    private Collider2D _col;
    private SpriteRenderer _renderer;
    private CharacterMovement _player;

    
    
    private void Awake()
    {
        _col= GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        
        if (_openWithKey)
        {
            _player = FindFirstObjectByType<CharacterMovement>();
        }
    }


    private void Update()
    {
        if (_openWithKey)
        {
            var distance = Vector3.Distance(transform.position, _player.transform.position);
            if (distance <= _distanceCheck /* playerhaskey*/)
            {
                Open();
                //Playerhaskey = false;
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
using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoBehaviour
{
    [FormerlySerializedAs("openWithKey")]
    [Header ("Opens With Key?")]
    [SerializeField] private bool _openWithKey;

    
    [Header("Open Sprite")]
    [SerializeField] private Sprite _openSprite;
    
    private Collider2D _col;
    private SpriteRenderer _renderer;
    private Player _player;

    
    
    private void Awake()
    {
        _col= GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (_openWithKey)
        {
            
        }
    }


    [Button]
    public void Open()
    {
        _renderer.sprite = _openSprite;
        _col.enabled = false;
    }
}
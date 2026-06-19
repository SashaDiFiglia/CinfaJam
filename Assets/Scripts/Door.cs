using System;
using Sirenix.OdinInspector;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    private Collider2D _col;
    private SpriteRenderer _renderer;
    
    [Header("Open Sprite")]
    [SerializeField] private Sprite _openSprite;


    private void Awake()
    {
        _col= GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }



    [Button]
    public void Open()
    {
        _renderer.sprite = _openSprite;
        _col.enabled = false;
    }
}
using System;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    private Collider _col;
    private SpriteRenderer _renderer;
    
    [Header("Open Sprite")]
    [SerializeField] private Sprite _openSprite;


    private void Awake()
    {
        _col= GetComponent<Collider>();
        _renderer = GetComponent<SpriteRenderer>();
    }



    public void Open()
    {
        _renderer.sprite = _openSprite;
        _col.enabled = false;
    }
}
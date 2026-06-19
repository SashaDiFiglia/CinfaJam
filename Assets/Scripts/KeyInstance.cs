using System;
using Unity.VisualScripting;
using UnityEngine;

public class KeyInstance : MonoBehaviour
{
    private PlayerKeyInventory _playerInventory;

    private void Awake()
    {
        _playerInventory = FindFirstObjectByType<PlayerKeyInventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerKeyInventory>())
        {
            _playerInventory.AddKey();
        }
        
        Destroy(this.gameObject);
    }
}
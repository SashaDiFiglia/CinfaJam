using System;
using Unity.VisualScripting;
using UnityEngine;

public class KeyInstance : MonoBehaviour
{
    private PlayerKeyInventory _playerInventory;

    [SerializeField] private float _checkDistance;

    private void Awake()
    {
        _playerInventory = FindFirstObjectByType<PlayerKeyInventory>();
    }

    private void Update()
    {
        var distance = Vector3.Distance(_playerInventory.transform.position, transform.position);
        if (distance <= _checkDistance)
        {
            PickUpKey();
        }
    }



    private void PickUpKey()
    {
        _playerInventory.AddKey();
        Destroy(this.gameObject);
    }
}
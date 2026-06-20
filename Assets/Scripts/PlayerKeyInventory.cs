using System;
using UnityEngine;
using UnityEngine.Serialization;

//Da Mettere sul Player


public class PlayerKeyInventory : MonoBehaviour
{
    [SerializeField] private int _holdKeys;
    
    public event Action<int> OnKeyAmountChange;

    public void ReduceKey()
    {
        _holdKeys--;
        
        OnKeyAmountChange?.Invoke(_holdKeys);
    }
    public void AddKey()
    {
        _holdKeys++;
        
        OnKeyAmountChange?.Invoke(_holdKeys);
    }
    public int GetHoldKeys()
    {
        return _heldKeys;
    }

}
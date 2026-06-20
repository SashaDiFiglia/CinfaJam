using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//Da Mettere sul Player


public class PlayerKeyInventory : MonoBehaviour
{ 
    [SerializeField] private int _heldKeys;

    public void ReduceKey()
    {
        _heldKeys--;
    }
    public void AddKey()
    {
        _heldKeys++;
    }
    public int GetHoldKeys()
    {
        return _heldKeys;
    }

}
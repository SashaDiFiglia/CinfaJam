using System.Collections.Generic;
using UnityEngine;
//Da Mettere sul Player


public class PlayerKeyInventory : MonoBehaviour
{
    [SerializeField] private int _holdKeys;

    public void ReduceKey()
    {
        _holdKeys--;
    }
    public void AddKey()
    {
        _holdKeys++;
    }
    public int GetHoldKeys()
    {
        return _holdKeys;
    }

}
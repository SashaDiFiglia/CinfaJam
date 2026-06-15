using System;
using System.Linq;
using UnityEngine;

public class PlayerHitter : MonoBehaviour
{
    [SerializeField] private HitType[] hitTypes;
    [SerializeField] private float damage;
    public enum HitType
    {
        TriggerEnter,
        TriggerExit,
        TriggerStay,
        CollisionEnter,
        CollisionExit,
        CollisionStay,
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hitTypes.Contains(HitType.TriggerEnter)) { return; }
        Debug.Log($"{other.gameObject.name} hit for {damage} damage - Trigger Enter");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!hitTypes.Contains(HitType.TriggerExit)) { return; }

        Debug.Log($"{other.gameObject.name} hit for {damage} damage - Trigger Exit");
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!hitTypes.Contains(HitType.TriggerStay)) { return; }

        Debug.Log($"{other.gameObject.name} hit for {damage} damage - Trigger Stay");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hitTypes.Contains(HitType.CollisionEnter)) { return; }

        Debug.Log($"{other.gameObject.name} hit for {damage} damage - Collision Enter");
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (!hitTypes.Contains(HitType.CollisionExit)) { return; }

        Debug.Log($"{other.gameObject.name} hit for {damage} damage - Collision Exit");
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (!hitTypes.Contains(HitType.CollisionStay)) { return; }
        Debug.Log($"{other.gameObject.name} hit for {damage} damage - Collision Stay");
    }
 
}

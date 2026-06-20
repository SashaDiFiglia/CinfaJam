using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    public bool IsFirst;

    public event Action<Checkpoint> OnPlayerEntered;

    private void Awake()
    {
        if (TryGetComponent<BoxCollider2D>(out var c))
        {
            c.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<CharacterInput>(out var character))
        {
            Debug.Log("entratp");

            OnPlayerEntered?.Invoke(this);
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;

        Handles.DrawWireCube(transform.position, Vector3.one * 0.5f);
    }

#endif
}
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;
    private float _lifetime;

    

    
    
    public void Setup(float damage, float lifetime)
    {
        _damage = damage;
        _lifetime = lifetime;
        StartCoroutine(DestroyAfterLifetime());
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        Hit(other.gameObject);
        DestroySelf();
    }


    void Hit(GameObject other)
    {
        Debug.Log($"Bullet hit {other.name}...");
        if (other.CompareTag("Player"))
        {
            Debug.Log($"...for {_damage} damage");
            other.GetComponent<CharacterHealth>().TakeDamage(_damage);
        }
        DestroySelf();
    }
    void DestroySelf()
    {
        Debug.Log($"Destroying bullet");
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(_lifetime);
        DestroySelf();
    }
    

}
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;
    private float _lifetime;
    private float _speed;
    
    public void Setup(float speed, float damage, float lifetime)
    {
        _damage = damage;
        _lifetime = lifetime;
        _speed = speed;
        StartCoroutine(DestroyAfterLifetime());
    }

    void Update() { transform.Translate(Vector2.up * (_speed * Time.deltaTime)); }

    void OnCollisionEnter2D(Collision2D col)
    {
        Hit(col.gameObject);
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
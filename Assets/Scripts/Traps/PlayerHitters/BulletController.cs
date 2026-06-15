using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region VARIABLES
        private float _damage;
        private float _lifetime;
        private float _speed;
        private bool _isMoving;
        private Coroutine _moveC;
    #endregion
    
    #region Setup
        public void Setup(float speed, float damage, float lifetime)
        {
            _damage = damage;
            _lifetime = lifetime;
            _speed = speed;
            _isMoving = true;
            _moveC = StartCoroutine(Move());
            StartCoroutine(DestroyAfterLifetime());
        }
    #endregion
    
    #region Movement controller
        private IEnumerator Move()
        {
            while (_isMoving) { transform.Translate(Vector2.up * (_speed * Time.deltaTime)); }
            yield return null;
        }
        public void StopMoving() { _isMoving = false; if (_moveC != null) { StopCoroutine(_moveC); _moveC = null; } }
    #endregion

    void OnCollisionEnter2D(Collision2D col) 
    { Hit(col.gameObject); }

    #region Hit/Self-Destruct
        void Hit(GameObject other)
        {
            if (other.CompareTag("Player")) 
            { other.GetComponent<CharacterHealth>()?.TakeDamage(_damage); }
            DestroySelf();
        }
        void DestroySelf() 
        { Destroy(gameObject); }

        private IEnumerator DestroyAfterLifetime()
        { yield return new WaitForSeconds(_lifetime); DestroySelf(); }
    #endregion
}
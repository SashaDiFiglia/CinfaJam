using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region VARIABLES
        private float _damage;
        private float _lifetime;
        private float _speed;
        private bool _isMoving;
        private bool _canExplode;
        private float _explosionRadius;
        private bool _canHitEnemies;
        private Coroutine _moveC;
        
        private Animator _animator;
        private bool _isPlayingAnimation;
        private string _animation = "BulletFlicker";
        private string _explodeAnim = "BulletExplosion";
        private float _explosionDuration = .1f;
        private Coroutine _loopAnimationC;
    #endregion
    
    #region Setup
        public void Setup(float speed, float damage, float lifetime, bool canExplode, bool hitsEnemies, float explosionRadius)
        {
            _damage = damage;
            _lifetime = lifetime;
            _speed = speed;
            _canExplode = canExplode;
            _canHitEnemies = hitsEnemies;
            _explosionRadius = explosionRadius;
            
            _animator = gameObject.GetComponent<Animator>();
            if (_animator == null) { Debug.LogWarning($"No animator on {gameObject.name}, animations will not be played"); }
            
            StopMoving();
            StopLoopAnimation();
            _moveC = StartCoroutine(Move());
            _loopAnimationC = StartCoroutine(LoopAnimation());
            StartCoroutine(DestroyAfterLifetime());
        }
    #endregion
    
    #region Movement controller
        private IEnumerator Move()
        {
            _isMoving = true;
            while (_isMoving)
            {
                transform.Translate(Vector2.up * (_speed * Time.deltaTime));
                yield return null;
            }
        }
        public void StopMoving() { _isMoving = false; if (_moveC != null) { StopCoroutine(_moveC); _moveC = null; } }
    #endregion
    
    #region Animator
        private IEnumerator LoopAnimation()
        {
            _isPlayingAnimation = true;
            while (_isPlayingAnimation)
            {
                _animator.Play(_animation, 0, 0.0f);
                yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length); 
            }
        }

        private void StopLoopAnimation()
        {
            _isPlayingAnimation = false;
            _animator.StopPlayback();
            if (_loopAnimationC != null) { StopCoroutine(_loopAnimationC); _loopAnimationC = null; }
        }

        private void PlayExplodeAnimation()
        {
            _animator.Play(_explodeAnim, 0, 0.0f);
            _explosionDuration = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }
    #endregion

    void OnCollisionEnter2D(Collision2D col) { Hit(col.gameObject); }

    #region Hit/Self-Destruct
        void Hit(GameObject other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CharacterHealth>()?.TakeDamage(_damage); 
                DestroySelf();
            }
            else if (_canHitEnemies)
            {
                other.GetComponent<Enemy>()?.TakeDamage(_damage);
                DestroySelf();
            }
           
        }
        void Explode() //deals damage in area.
        { 
            Collider2D col = gameObject.GetComponent<Collider2D>();
            Collider2D[] cols = Physics2D.OverlapCircleAll(col.bounds.center, _explosionRadius);
            foreach (Collider2D c in cols)
            {
                if (c.CompareTag("Player")) { c.GetComponent<CharacterHealth>()?.TakeDamage(_damage); }
                if (_canHitEnemies) { c.GetComponent<Enemy>()?.TakeDamage(_damage); }
            }
            StopLoopAnimation();
            PlayExplodeAnimation();
            StartCoroutine(WaitForDestruction());
        }

        private IEnumerator WaitForDestruction()
        {
            yield return new WaitForSeconds(_explosionDuration);
            Destroy(gameObject);
        }



        void DestroySelf()
        {
            if (_canExplode) { StopMoving(); Explode(); return;}
            Destroy(gameObject);
        }

        private IEnumerator DestroyAfterLifetime()
        { yield return new WaitForSeconds(_lifetime); DestroySelf(); }
    #endregion
    
}
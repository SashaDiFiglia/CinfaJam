using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region VARIABLES
        private float _damage;
        private float _lifetime;
        private float _speed;
        private bool _isMoving;
        private bool _canExplode;
        private bool _canHitEnemies;
        private Coroutine _moveC;
        
        private Animator _animator;
        private bool _isPlayingAnimation;
        private string _animation = "BulletFlicker";
        private Coroutine _loopAnimationC;
    #endregion
    
    #region Setup
        public void Setup(float speed, float damage, float lifetime, bool canExplode, bool hitsEnemies)
        {
            _damage = damage;
            _lifetime = lifetime;
            _speed = speed;
            _canExplode = canExplode;
            _canHitEnemies = hitsEnemies;
            
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
            if (_loopAnimationC != null) { StopCoroutine(_loopAnimationC); _loopAnimationC = null; }
        }
    #endregion

    void OnCollisionEnter2D(Collision2D col) { Hit(col.gameObject); }

    #region Hit/Self-Destruct
        void Hit(GameObject other)
        {
            if (other.CompareTag("Player")) 
            { other.GetComponent<CharacterHealth>()?.TakeDamage(_damage); }
            DestroySelf();
        }
        void Explode() //deals damage in area.
        { 
            Collider2D col = gameObject.GetComponent<Collider2D>();
            Collider2D[] cols = Physics2D.OverlapCircleAll(col.bounds.center, col.bounds.extents.x);
            foreach (Collider2D c in cols)
            {
                if (c.CompareTag("Player")) { c.GetComponent<CharacterHealth>()?.TakeDamage(_damage); }
                if (_canHitEnemies) { Debug.Log("Hit");}
            }

        }

        void DestroySelf()
        {
            if (_canExplode) {Explode();}
            Destroy(gameObject);
        }

        private IEnumerator DestroyAfterLifetime()
        { yield return new WaitForSeconds(_lifetime); DestroySelf(); }
    #endregion
    
    #region Odin Buttons
        [Button] public void StartFlicker(float speed, float damage, float lifetime, bool canExplode, bool hitsEnemies) 
        { Setup(speed, damage, lifetime, canExplode, hitsEnemies); }
    #endregion
}
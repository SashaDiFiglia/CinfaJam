using System.Collections;
using UnityEngine;

public class HazardController : MonoBehaviour
{
    #region VARIABLES - HAZARD
        private bool _dealsDamageOverTime;
        
        private float _damage;
        private float _delay;

        private bool _canHit;
        private Collider2D _col;
        private Coroutine _hitDelayC;
    #endregion
    
    #region VARIABLES - ANIMATION
        private Animator _anim;
        
        private string _forwardAnim = "SpikeMovePlayback";
        private string _backwardsAnim = "SpikeMoveBackwards";
        private string _upAnim = "SpikeFlicker";
        
        private Coroutine _waitForFlickerC = null;
        private Coroutine _flickerForDurationC = null;
        
        private bool _isFlickering;
    #endregion  
    
    #region Setup
        public void Setup(float value, float delayBetweenHits, bool canDamageOverTime)
        {
            _damage = value;
            _delay = delayBetweenHits;
            _dealsDamageOverTime = canDamageOverTime;
            _canHit = true;
            _col = gameObject.GetComponent<Collider2D>(); 
            
            _anim = gameObject.GetComponent<Animator>();
            if (_anim == null) 
            { Debug.LogWarning($"No animator on {gameObject.name}, animations will not be played"); }
        }
    #endregion
    
    public void ToggleSpikes(bool activating)
    {
        if (_anim != null) { MoveSpikes(activating); }
        _col.enabled = activating;
    }

    #region Animator - Move Spikes
        private void MoveSpikes(bool isForward)
        {
            _isFlickering = false;
                
            if (_waitForFlickerC != null)     { StopCoroutine(_waitForFlickerC);     _waitForFlickerC     = null; }
            if (_flickerForDurationC != null) { StopCoroutine(_flickerForDurationC); _flickerForDurationC = null; }
                
            if (isForward)
            { 
                _anim.Play(_forwardAnim);
                _waitForFlickerC = StartCoroutine(WaitForFlicker
                    (_anim.GetCurrentAnimatorClipInfo(0)[0].clip.length)); 
            }
            else 
            { _anim.Play(_backwardsAnim); }
        }
    #endregion
    
    #region Animator - Flicker
        private IEnumerator LoopFlicker()
        {
            while (_isFlickering)
            {
                _anim.Play(_upAnim, 0, 0.0f);
                yield return new WaitForSeconds(_anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            }
        }
        
        private IEnumerator WaitForFlicker(float waitTime)
        {
            yield return new WaitForSeconds(waitTime); 
            if (_flickerForDurationC != null) { StopCoroutine(_flickerForDurationC); _flickerForDurationC = null; }
            _isFlickering = true;
            _flickerForDurationC = StartCoroutine(LoopFlicker());
        }
    #endregion
    
    #region Hit Detection
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<CharacterHealth>() == null) { return; }
            ClearCoroutine(); Hit(other.gameObject);
        }
        private void OnTriggerStay(Collider other)
        {
            if (!_dealsDamageOverTime) { return; }
            if (other.GetComponent<CharacterHealth>() == null) { return; }
            if (!_canHit) { return; } Hit(other.gameObject);
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<CharacterHealth>() == null) { return; } 
            ClearCoroutine(); 
        }
    #endregion
    
    #region Hit Damage
        void Hit(GameObject player)
        {
            _canHit = false;
            player.GetComponent<CharacterHealth>()?.TakeDamage(_damage);
            Debug.Log($"Spikes hit {player.name} for {_damage} damage");
            StartCoroutine(Delay());
        }

        public void CanDamageOverTime(bool canDamageOverTime) 
        { _dealsDamageOverTime = canDamageOverTime; }
    #endregion
    
    #region Coroutines
        private IEnumerator Delay()
        { yield return new WaitForSeconds(_delay); _canHit = true; }
        private void ClearCoroutine()
        { if (_hitDelayC != null) { StopCoroutine(_hitDelayC); _hitDelayC = null; _canHit = true; } }
    #endregion
    
}

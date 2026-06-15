using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField]  private Sprite _deactivatedSpikeSprite;
    
    #region VARIABLES - SPIKE
        private float _damage;
        private float _delay;

        private bool _canHit;
        private Collider2D _col;
        private Coroutine _hitDelayC;
    #endregion
    
    #region VARIABLES - ANIMATION
        private Animator _anim;
        
        private string _spikeMoveForwardAnim = "SpikeMovePlayback";
        private string _spikeMoveBackwardsAnim = "SpikeMoveBackwards";
        private string _spikeFlickerAnim = "SpikeFlicker";
        
        private Coroutine _waitForFlickerC = null;
        private Coroutine _flickerForDurationC = null;
        
        private bool _isFlickering;
    #endregion  
    
    #region Setup
        public void Setup(float value, float delayBetweenHits)
        {
            Debug.Log("Setup Spikes");
            _damage = value;
            _delay = delayBetweenHits;
            _canHit = true;
            _col = gameObject.GetComponent<Collider2D>(); 
            _anim = gameObject.GetComponent<Animator>();
        }
    #endregion
    
    public void ToggleSpikes(bool activating)
    {
        MoveSpikes(activating);
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
                _anim.Play(_spikeMoveForwardAnim);
                _waitForFlickerC = StartCoroutine(WaitForFlicker
                    (_anim.GetCurrentAnimatorClipInfo(0)[0].clip.length)); 
            }
            else 
            { _anim.Play(_spikeMoveBackwardsAnim); }
        }
    #endregion
    
    #region Animator - Flicker
        private IEnumerator LoopFlicker()
        {
            while (_isFlickering)
            {
                _anim.Play(_spikeFlickerAnim, 0, 0.0f);
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
            if (!other.CompareTag("Player")) { return; }
            ClearCoroutine(); Hit(other.gameObject);
        }
        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) { return; }
            if (!_canHit) { return; } Hit(other.gameObject);
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) { return; } 
            ClearCoroutine(); 
        }
    #endregion
    
    #region Hit & Coroutines
        void Hit(GameObject player)
        {
            _canHit = false;
            player.GetComponent<CharacterHealth>()?.TakeDamage(_damage);
            Debug.Log($"Spikes hit {player.name} for {_damage} damage");
            StartCoroutine(Delay());
        }
    
        private IEnumerator Delay()
        { yield return new WaitForSeconds(_delay); _canHit = true; }
        private void ClearCoroutine()
        { if (_hitDelayC != null) { StopCoroutine(_hitDelayC); _hitDelayC = null; _canHit = true; } }
    #endregion
    

}

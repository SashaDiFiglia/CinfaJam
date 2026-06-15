using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private float _damage;
    private float _delay;

    private bool _canHit;
    private Coroutine _hitDelayC;
    
    
    public void Setup(float value, float delayBetweenHits)
    {
        _damage = value;
        _delay = delayBetweenHits;
        _canHit = true;
        ToggleSpikes(false);
    }
    
    public void ToggleSpikes(bool activating)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = activating;
        gameObject.GetComponent<Collider2D>().enabled = activating;
    }

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

}

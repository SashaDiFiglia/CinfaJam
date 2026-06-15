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
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        ClearCoroutine();
        _canHit = true;
        Hit(other.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!_canHit) { return; }
        Hit(other.gameObject);
    }

    void Hit(GameObject other)
    {
        Debug.Log($"Spikes hit {other.name} for {_damage} damage");
        _canHit = false;
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);
        _canHit = true;
    }
    private void ClearCoroutine()
    { if (_hitDelayC != null) { StopCoroutine(_hitDelayC); _hitDelayC = null; } }

}

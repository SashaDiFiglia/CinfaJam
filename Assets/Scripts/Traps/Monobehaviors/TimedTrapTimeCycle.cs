using System.Collections;
using UnityEngine;

public class TimedTrapTimeCycle: MonoBehaviour
{
    [SerializeField] private AActivableTrap trap;
    [SerializeField] private float waitTime;
    [SerializeField] private float activeTime;
    
    
    private bool _isActive;
    private Coroutine _c;

    void Start()     { trap.Setup(this); Clear(); ToggleTrap();}
    void OnDestroy() { StopCoroutine(_c); trap.Clear(); }
    void OnDisable() { StopCoroutine(_c); trap.Clear(); }
    
    void Clear() { _isActive = false; StopCoroutine(_c); }
    
    public void ToggleTrap()
    {
        _isActive = !_isActive;
        if (_isActive && _c == null) { _c = StartCoroutine(ActivateTrap()); }
        else if (_c != null)         { StopCoroutine(_c); _c = null; trap.DeactivateTrap(); }
    }

    private IEnumerator ActivateTrap()
    {
        while (_isActive)
        {
            yield return new WaitForSeconds(waitTime);
            trap.ActivateTrap();
            yield return new WaitForSeconds(activeTime);
            trap.DeactivateTrap();
        }
    }
}

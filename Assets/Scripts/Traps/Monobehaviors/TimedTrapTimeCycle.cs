using System.Collections;
using UnityEngine;

public class TimedTrapTimeCycle: MonoBehaviour
{
    [SerializeField] private AActivableTrap trap;

    
    private bool _isActive;
    private Coroutine _c;

    void Start()
    {
        trap.Setup(this);
    }
    
    
    public void ToggleTrap(float waitTime, float activeTime)
    {
        _isActive = !_isActive;
        if (_isActive && _c == null) { _c = StartCoroutine(ActivateTrap(waitTime, activeTime)); }
        else if (_c != null)         { StopCoroutine(_c); _c = null; trap.DeactivateTrap(); }
    }

    private IEnumerator ActivateTrap(float waitTime, float activeTime)
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

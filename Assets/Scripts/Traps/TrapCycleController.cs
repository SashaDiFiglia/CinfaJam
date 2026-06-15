using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class TrapCycleController: MonoBehaviour
{
    [SerializeField] private AActivableTrap trap;
    [SerializeField] private float waitTime;
    [SerializeField] private float activeTime;
    
    
    private bool _isActive;
    private Coroutine _c;

    void Start()     { trap.Setup(this); Clear(); ToggleTrap();}
    void OnDestroy() { Clear(); trap.Clear(); }
    void OnDisable() { Clear(); trap.Clear(); }
    
    void Clear() { _isActive = false; if (_c != null) { StopCoroutine(_c); _c = null;} }
    
    public void ToggleTrap()
    {
        _isActive = !_isActive;
        if (_isActive && _c == null) { _c = StartCoroutine(TrapCycle()); }
        else if (_c != null)         { StopCoroutine(_c); _c = null; trap.DeactivateTrap(); }
    }

    private IEnumerator TrapCycle()
    {
        while (_isActive)
        {
            yield return new WaitForSeconds(waitTime);
            trap.ActivateTrap();
            yield return new WaitForSeconds(activeTime);
            trap.DeactivateTrap();
        }
    }
    
    [Button] public void Setup()          { Clear(); trap.Setup(this); }
    [Button] public void ActivateTrap()   { trap.ActivateTrap(); }
    [Button] public void DeactivateTrap() { trap.DeactivateTrap(); }
}

using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class TrapCycleController: MonoBehaviour
{
    [SerializeField] private AActivableTrap trap;
    
    [Header("Time Settings")]
    public float timeBeforeActivation;
    public float timeBeforeDeactivation;
    
    private bool _isActive;
    private Coroutine _c;

    #region Start/Destroy
        void Start()     { trap.Setup(this); Clear(); ToggleTrapCycle();}
        void OnDestroy() { Clear(); trap.Clear(); }
        void OnDisable() { Clear(); trap.Clear(); }
    #endregion
    
    #region Trap Cycle
        private IEnumerator TrapCycle()
        {
            while (_isActive)
            {
                yield return new WaitForSeconds(timeBeforeActivation);
                trap.ActivateTrap();
                yield return new WaitForSeconds(timeBeforeDeactivation);
                trap.DeactivateTrap();
            }
        }
        
        [Button]
        public void ToggleTrapCycle()
        {
            _isActive = !_isActive;
            if (_isActive && _c == null) { _c = StartCoroutine(TrapCycle()); }
            else if (_c != null)         { StopCoroutine(_c); _c = null; trap.DeactivateTrap(); }
        }
        
        void Clear() { _isActive = false; if (_c != null) { StopCoroutine(_c); _c = null;} }
    #endregion
    
    #region Odin Buttons
        [Button] public void Setup()          { Clear(); trap.Setup(this); }
        [Button] public void ActivateTrap()   { trap.ActivateTrap(); }
        [Button] public void DeactivateTrap() { trap.DeactivateTrap(); }
    #endregion
}

using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class TrapCycleController: MonoBehaviour
{
    private ITrap _trap;
    
    
    [Header("Time Settings")]
    public bool isTimeBased;
    public float timeBeforeActivation;
    public float timeBeforeDeactivation;
    
    private bool _isActive;
    private Coroutine _c;

    #region Start/Destroy
        void Start()
        {
            _trap = GetComponent<ITrap>();
            Clear(); ToggleTrapCycleMaster(); 
        }
        void OnDestroy() { Clear(); }
        void OnDisable() { Clear();  }
    #endregion
    
    #region Trap Cycle
        [Button]
        public void ToggleTrapCycleMaster()
        {
            _isActive = !_isActive;
       
            if (!isTimeBased) { TrapCycleToggle(); }
            else              { TrapCycleTimeBased(); }
        }

        private void TrapCycleToggle()
        {
            if (_isActive) { _trap.ActivateTrap(); }
            else           { _trap.DeactivateTrap(); }
        }

        private void TrapCycleTimeBased()
        {
            if (_isActive && _c == null) { _c = StartCoroutine(TrapCycle()); }
            else if (_c != null)         { StopCoroutine(_c); _c = null; _trap.DeactivateTrap(); }
        }
  
        private IEnumerator TrapCycle()
        {
            while (_isActive)
            {
                yield return new WaitForSeconds(timeBeforeActivation);
                _trap.ActivateTrap();
                yield return new WaitForSeconds(timeBeforeDeactivation);
                _trap.DeactivateTrap();
            }
        }
        
        void Clear() { _isActive = false; if (_c != null) { StopCoroutine(_c); _c = null;} }
    #endregion
    
    #region Odin Buttons
        [Button] public void ActivateTrap()   { _trap.ActivateTrap(); }
        [Button] public void DeactivateTrap() { _trap.DeactivateTrap(); }
    #endregion
}

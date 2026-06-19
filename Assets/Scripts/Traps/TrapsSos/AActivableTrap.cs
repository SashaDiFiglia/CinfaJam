using UnityEngine;

public abstract class AActivableTrap : ScriptableObject
{
    #region Setup/Clear
    protected TrapCycleController trapController;
    public void Setup(TrapCycleController trapCycleController) { trapController = trapCycleController; OnSetup(); }
    public void Clear() { OnClear(); trapController = null; }
    protected virtual void OnSetup() {}
    protected virtual void OnClear() {}
    #endregion
    
    public abstract void ActivateTrap();
    public virtual void DeactivateTrap() {}
}

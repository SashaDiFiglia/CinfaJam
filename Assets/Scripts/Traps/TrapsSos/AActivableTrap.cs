using UnityEngine;

public abstract class AActivableTrap : ScriptableObject
{
    protected TrapCycleController trapController;
    public void Setup(TrapCycleController trapCycleController) { trapController = trapCycleController; OnSetup(); }
    public void Clear() { trapController = null; OnClear();}
    protected virtual void OnSetup() {}
    protected virtual void OnClear() {}
    
    
    
    public abstract void ActivateTrap();
    public abstract void DeactivateTrap();
}

using UnityEngine;

public abstract class AActivableTrap : ScriptableObject
{
    protected TimedTrapTimeCycle trapController;
    public void Setup(TimedTrapTimeCycle trapTimeCycle) { trapController = trapTimeCycle; OnSetup(); }
    public void Clear() { trapController = null; OnClear();}
    protected virtual void OnSetup() {}
    protected virtual void OnClear() {}
    
    
    
    public abstract void ActivateTrap();
    public abstract void DeactivateTrap();
}

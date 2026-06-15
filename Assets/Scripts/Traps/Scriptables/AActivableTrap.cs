using UnityEngine;

public abstract class AActivableTrap : ScriptableObject
{
    protected TimedTrapTimeCycle trapController;
    public void Setup(TimedTrapTimeCycle trapTimeCycle) { trapController = trapTimeCycle; }
    public abstract void ActivateTrap();
    public abstract void DeactivateTrap();
}

using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;


[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitUntilTimer",
    story: "[Self] stays idle until [IdleTime] seconds elapsed then resets [IsIdle]", category: "Action",
    id: "9e192de96cf00344a6f8e2c349ea3642")]
public partial class WaitUntilTimerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> IdleTime;
    [SerializeReference] public BlackboardVariable<bool> IsIdle;

    protected override Status OnStart()
    {
        return Status.Success;
    }
}
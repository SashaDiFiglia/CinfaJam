using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TryMoveAway", story: "[Self] tries to move away from [Target]",
    category: "Action/Transform",
    id: "04f3c36481d5b3d937adc0c897b7931f")]
public partial class TryMoveAwayAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        return Status.Success;
    }
}
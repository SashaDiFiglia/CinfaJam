using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TryMoveTowards", story: "[Self] tries to move to [Target]", category: "Action",
    id: "4e5ef58c07f29a84aba79650a755ca9c")]
public partial class TryMoveTowardsAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        return Status.Success;
    }
}
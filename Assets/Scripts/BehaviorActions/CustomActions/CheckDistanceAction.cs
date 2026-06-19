using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Check distance", story: "Checks if distance between [Self] and [Target] <= [Distance]", category: "Action",
    id: "2c749044ee2e481dcdb79d4d5c90717f")]
public partial class CheckDistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Distance;

    protected override Status OnStart()
    {
        return Vector3.Distance(Self.Value.transform.position, Target.Value.transform.position) <= Distance.Value
            ? Status.Success
            : Status.Failure;
    }
}
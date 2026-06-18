using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Enemy Attack", story: "[Self] Attacks Target", category: "Action",
    id: "2c749044ee2e481dcdb79d4d5c92717f")]
public partial class EnemyAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;


    protected override Status OnStart()
    {
        if (!Self.Value.TryGetComponent<Enemy>(out var enemy))
        {
            return Status.Failure;
        }

        enemy.Attack();
        return Status.Success;
    }
}
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Custom Timer", story: "Wait for [Duration] seconds", category: "Action",
    id: "2c749044ee2e481dcdb73d4d5c90717f")]
public class CustomTimerAction : Modifier
{
    [SerializeReference] public BlackboardVariable<float> Duration;

    private float m_Timer;
    private float _elapsed;

    protected override Status OnStart()
    {
        if (Child == null)
        {
            LogFailure("No child node to timeout for.");
            return Status.Failure;
        }

        m_Timer = Duration.Value;
        _elapsed += Time.fixedDeltaTime;

        if (_elapsed < m_Timer)
        {
            return Status.Failure;
        }

        _elapsed = 0.0f;

        var childStatus = StartNode(Child);

        if (childStatus == Status.Success || childStatus == Status.Failure)
        {
            EndNode(Child);
        }

        return childStatus;
    }
}
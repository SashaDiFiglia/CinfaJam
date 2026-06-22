using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveTowards", story: "Move [Agent] towards [Target] by [UnitPerSecond] using [Component]",
    category: "Action/Transform", id: "ad7a6ceb8e97fbb45d3c9fc0317719b2")]
public partial class MoveTowardsAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> UnitPerSecond;
    [SerializeReference] public BlackboardVariable<Enemy> Component;

    private const float Angle = 10f;
    private const float Distance = 1f;
    private const float Offset = 0.5f;
    private const int HalfRays = 3;


    protected override Status OnStart()
    {
        Vector2 targetDirection = (Target.Value.position - Agent.Value.position).normalized;
        Vector2 avoidanceDirection = Vector2.zero;
        int obstaclesHit = 0;

        for (int i = -HalfRays; i <= HalfRays; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(Angle * i, Vector3.forward);
            Vector2 rotatedVector = (rotation * targetDirection).normalized;
            
            Vector2 origin = (Vector2)Agent.Value.position + (rotatedVector * Offset);

            var hit = Physics2D.Raycast(origin, rotatedVector, Distance);

            Debug.DrawRay(origin, rotatedVector * Distance, hit ? Color.red : Color.green);

            if (!hit || hit.collider.gameObject == Agent.Value.gameObject)
            {
                continue;
            }

            Vector2 distanceToObstacle = hit.point - (Vector2)Agent.Value.position;

            // Più siamo vicini, maggiore è il peso della deviazione
            float strength = 1.0f - (distanceToObstacle.magnitude / Distance);

            avoidanceDirection -= rotatedVector * strength;
            obstaclesHit++;
        }

        Vector2 finalDirection = targetDirection + avoidanceDirection;

        Component.Value.Move(finalDirection);

        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}
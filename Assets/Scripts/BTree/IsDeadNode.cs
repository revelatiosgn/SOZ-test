using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

[CreateAssetMenu(fileName = "IsDeadNode", menuName = "BTree/Actions/IsDead")]
public sealed class IsDeadNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();

        EcsPool<HealthData> healthPool = world.GetPool<HealthData>();
        ref HealthData healthData = ref healthPool.Get(blackboard.Entity);

        return healthData.Health <= 0;
    }
}

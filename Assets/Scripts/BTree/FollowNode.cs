using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

[CreateAssetMenu(fileName = "FollowNode", menuName = "BTree/Actions/Follow")]
public sealed class FollowNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();

        EcsPool<CombatData> combatPool = world.GetPool<CombatData>();
        int targetEntity = combatPool.Get(blackboard.Entity).TargetEnitity;

        EcsPool<NavAgentData> navAgentDataPool = world.GetPool<NavAgentData>();
        EcsPool<TransformData> transformPool = world.GetPool<TransformData>();

        ref NavAgentData navAgentData = ref navAgentDataPool.Get(blackboard.Entity);
        ref TransformData targetTransform = ref transformPool.Get(targetEntity);

        navAgentData.NavMeshAgent.destination = targetTransform.Transform.position;
        navAgentData.NavMeshAgent.isStopped = false;

        return true;
    }
}

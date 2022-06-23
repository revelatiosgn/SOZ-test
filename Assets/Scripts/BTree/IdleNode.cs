using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleNode", menuName = "BTree/Actions/Idle")]
public sealed class IdleNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();

        EcsPool<NavAgentData> navAgentPool = world.GetPool<NavAgentData>();
        ref NavAgentData navAgentData = ref navAgentPool.Get(blackboard.Entity);
        navAgentData.NavMeshAgent.isStopped = true;

        return true;
    }
}

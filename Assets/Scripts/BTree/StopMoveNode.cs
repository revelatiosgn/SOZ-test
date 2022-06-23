using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StopMoveNode", menuName = "BTree/Actions/StopMove")]
public sealed class StopMoveNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();

        EcsPool<NavAgentData> navAgentDataPool = world.GetPool<NavAgentData>();
        ref NavAgentData navAgentData = ref navAgentDataPool.Get(blackboard.Entity);
        navAgentData.NavMeshAgent.isStopped = true;

        return true;
    }
}

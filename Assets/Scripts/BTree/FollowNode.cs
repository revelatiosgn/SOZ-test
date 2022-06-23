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

        ref CombatData selfCombatData = ref combatPool.Get(blackboard.Entity);
        if (selfCombatData.AttackDelayTimer > 0)
            return false;

        int targetEntity = combatPool.Get(blackboard.Entity).TargetEnitity;
        if (targetEntity == -1)
            return false;

        EcsPool<NavAgentData> navAgentPool = world.GetPool<NavAgentData>();
        EcsPool<TransformData> transformPool = world.GetPool<TransformData>();

        ref NavAgentData navAgentData = ref navAgentPool.Get(blackboard.Entity);
        ref TransformData targetTransform = ref transformPool.Get(targetEntity);

        navAgentData.NavMeshAgent.destination = targetTransform.Transform.position;
        navAgentData.NavMeshAgent.isStopped = false;

        return true;
    }

    private bool Check(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();

        EcsPool<CombatData> combatPool = world.GetPool<CombatData>();

        ref CombatData selfCombatData = ref combatPool.Get(blackboard.Entity);

        int targetEntity = selfCombatData.TargetEnitity;
        if (targetEntity == -1)
            return false;

        EcsPool<VisionData> visionPool = world.GetPool<VisionData>();
        ref VisionData visionData = ref visionPool.Get(blackboard.Entity);
        if (visionData.VisibleEntities.IndexOf(targetEntity) == -1)
            return true;

        EcsPool<TransformData> transformPool = world.GetPool<TransformData>();
        ref TransformData selfTransform = ref transformPool.Get(blackboard.Entity);
        ref TransformData targetTransform = ref transformPool.Get(targetEntity);

        float attackRange = 5f;
        float sqrRange = Vector3.SqrMagnitude(targetTransform.Transform.position - selfTransform.Transform.position);
        if (sqrRange > attackRange * attackRange)
            return false;

        return true;
    }

    private void Action(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();

        EcsPool<CombatData> combatPool = world.GetPool<CombatData>();
        int targetEntity = combatPool.Get(blackboard.Entity).TargetEnitity;

        EcsPool<NavAgentData> navAgentPool = world.GetPool<NavAgentData>();
        EcsPool<TransformData> transformPool = world.GetPool<TransformData>();

        ref NavAgentData navAgentData = ref navAgentPool.Get(blackboard.Entity);
        ref TransformData targetTransform = ref transformPool.Get(targetEntity);

        navAgentData.NavMeshAgent.destination = targetTransform.Transform.position;
        navAgentData.NavMeshAgent.isStopped = false;
    }
}

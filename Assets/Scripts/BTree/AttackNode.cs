using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackNode", menuName = "BTree/Actions/Attack")]
public sealed class AttackNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        if (!Check(blackboard))
            return false;

        Action(blackboard);

        return true;
    }

    private bool Check(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();

        EcsPool<CombatData> combatPool = world.GetPool<CombatData>();

        ref CombatData selfCombatData = ref combatPool.Get(blackboard.Entity);
        if (selfCombatData.AttackDelayTimer > 0)
            return false;

        int targetEntity = selfCombatData.TargetEnitity;
        if (targetEntity == -1)
            return false;

        EcsPool<VisionData> visionPool = world.GetPool<VisionData>();
        ref VisionData visionData = ref visionPool.Get(blackboard.Entity);
        if (visionData.VisibleEntities.IndexOf(targetEntity) == -1)
            return false;

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
        ref CombatData selfCombatData = ref combatPool.Get(blackboard.Entity);
        int targetEntity = selfCombatData.TargetEnitity;

        EcsPool<TransformData> transformPool = world.GetPool<TransformData>();
        ref TransformData selfTransform = ref transformPool.Get(blackboard.Entity);
        ref TransformData targetTransform = ref transformPool.Get(targetEntity);

        EcsPool<NavAgentData> navAgentPool = world.GetPool<NavAgentData>();
        ref NavAgentData navAgentData = ref navAgentPool.Get(blackboard.Entity);
        navAgentData.NavMeshAgent.isStopped = true;

        selfTransform.Transform.LookAt(targetTransform.Transform.position, Vector3.up);

        MyEventBus.OnAttackBegin?.Invoke(blackboard.Entity);
    }
}

using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetInRangeNode", menuName = "BTree/Actions/TargetInRange")]
public sealed class TargetInRangeNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();

        EcsPool<CombatData> combatPool = world.GetPool<CombatData>();
        ref CombatData selfCombatData = ref combatPool.Get(blackboard.Entity);

        EcsPool<TransformData> transformPool = world.GetPool<TransformData>();
        ref TransformData selfTransform = ref transformPool.Get(blackboard.Entity);
        ref TransformData targetTransform = ref transformPool.Get(selfCombatData.TargetEnitity);

        float attackRange = selfCombatData.AttackRange;
        float sqrRange = Vector3.SqrMagnitude(targetTransform.Transform.position - selfTransform.Transform.position);

        return sqrRange <= attackRange * attackRange;
    }
}

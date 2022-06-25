using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "LookAtTargetNode", menuName = "BTree/Actions/LookAtTarget")]
public sealed class LookAtTargetNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();

        EcsPool<CombatData> combatPool = world.GetPool<CombatData>();
        ref CombatData selfCombatData = ref combatPool.Get(blackboard.Entity);
        int targetEntity = selfCombatData.TargetEnitity;

        EcsPool<TransformData> transformPool = world.GetPool<TransformData>();
        ref TransformData selfTransform = ref transformPool.Get(blackboard.Entity);
        ref TransformData targetTransform = ref transformPool.Get(targetEntity);

        Vector3 pos = targetTransform.Transform.position;
        pos.y = selfTransform.Transform.position.y;
        selfTransform.Transform.LookAt(pos, Vector3.up);

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectTargetNode", menuName = "BTree/Actions/SelectTarget")]
public sealed class SelectTargetNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();

        EcsPool<CombatData> combatPool = world.GetPool<CombatData>();
        ref CombatData combatData = ref combatPool.Get(blackboard.Entity);

        if (combatData.TargetEnitity != -1)
            return false;

        EcsPool<VisionData> visionPool = world.GetPool<VisionData>();
        EcsPool<TransformData> transformPool = world.GetPool<TransformData>();
        EcsPool<HealthData> healthPool = world.GetPool<HealthData>();
        
        ref VisionData visionData = ref visionPool.Get(blackboard.Entity);
        if (visionData.VisibleEntities.Count == 0)
            return false;

        ref TransformData selfTransform = ref transformPool.Get(blackboard.Entity);
        float minSqrRange = float.MaxValue;
        foreach (int visibleEntity in visionData.VisibleEntities)
        {
            ref HealthData healthData = ref healthPool.Get(visibleEntity);
            if (healthData.IsDead)
                continue;

            ref TransformData visibleTransform = ref transformPool.Get(visibleEntity);
            float sqrRange = Vector3.SqrMagnitude(selfTransform.Transform.position - visibleTransform.Transform.position);
            if (sqrRange < minSqrRange)
            {
                sqrRange = minSqrRange;
                combatData.TargetEnitity = visibleEntity;
            }
        }

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetVisibleNode", menuName = "BTree/Actions/TargetVisible")]
public sealed class TargetVisibleNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();

        EcsPool<CombatData> combatPool = world.GetPool<CombatData>();
        ref CombatData selfCombatData = ref combatPool.Get(blackboard.Entity);
        
        EcsPool<VisionData> visionPool = world.GetPool<VisionData>();
        ref VisionData visionData = ref visionPool.Get(blackboard.Entity);

        return visionData.VisibleEntities.IndexOf(selfCombatData.TargetEnitity) != -1;
    }
}

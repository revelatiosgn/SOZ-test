using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "HaveTargetNode", menuName = "BTree/Actions/HaveTarget")]
public sealed class HaveTargetNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();
        
        EcsPool<CombatData> combatPool = world.GetPool<CombatData>();
        ref CombatData selfCombatData = ref combatPool.Get(blackboard.Entity);

        return selfCombatData.TargetEnitity != -1;
    }
}

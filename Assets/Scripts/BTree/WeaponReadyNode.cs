using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponReadyNode", menuName = "BTree/Actions/WeaponReady")]
public sealed class WeaponReadyNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        EcsWorld world = blackboard.Systems.GetWorld();
        
        EcsPool<CombatData> combatPool = world.GetPool<CombatData>();
        ref CombatData selfCombatData = ref combatPool.Get(blackboard.Entity);

        return selfCombatData.AttackDelayTimer <= 0f;
    }
}

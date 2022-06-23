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
        MyEventBus.OnAttackBegin?.Invoke(blackboard.Entity);

        return true;
    }
}

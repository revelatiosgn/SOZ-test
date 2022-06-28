using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackNode", menuName = "BTree/Actions/Attack")]
public sealed class AttackNode : BTActionNode
{
    public override bool Evaluate(BTBlackboard blackboard)
    {
        EventsBuffer eventsBuffer = blackboard.Systems.GetShared<SharedData>().EventsBuffer;
        eventsBuffer.NewEvent<AttackBeginEvent>().AttackerEnity = blackboard.Entity;

        return true;
    }
}

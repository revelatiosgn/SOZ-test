using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<PlayerTag, NavAgentData>> _filter = default;
    private readonly EcsPoolInject<CombatData> _combatPool = default;
    private readonly EcsPoolInject<NavAgentData> _navAgentPool = default;

    public void Run(EcsSystems systems)
    {
        EventsBuffer eventsBuffer = systems.GetShared<SharedData>().EventsBuffer;

        foreach (var eventEnitiy in eventsBuffer.GetEventBodies<PlayerMoveEvent>(out var eventsPool))
        {
            ref PlayerMoveEvent playerMoveEvent = ref eventsPool.Get(eventEnitiy);

            foreach (int entity in _filter.Value)
            {
                ref NavAgentData navAgentData = ref _navAgentPool.Value.Get(entity);
                navAgentData.NavMeshAgent.destination = playerMoveEvent.Destination;
                navAgentData.NavMeshAgent.isStopped = false;

                ref CombatData combatData = ref _combatPool.Value.Get(entity);
                combatData.TargetEnitity = -1;
            }
        }
    }
}

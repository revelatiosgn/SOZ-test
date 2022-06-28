using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class MoveSystem : IEcsRunSystem
{
    private readonly EcsPoolInject<NavAgentData> _navAgentPool = default;

    public void Run(EcsSystems systems)
    {
        EventsBuffer eventsBuffer = systems.GetShared<SharedData>().EventsBuffer;
        
        foreach (var eventEnitiy in eventsBuffer.GetEventBodies<DeathEvent>(out var eventsPool))
        {
            int deadEntity = eventsPool.Get(eventEnitiy).DeadEntity;
            ref NavAgentData navAgentData = ref _navAgentPool.Value.Get(deadEntity);
            navAgentData.NavMeshAgent.isStopped = true;
            navAgentData.NavMeshAgent.enabled = false;
        }
    }
}

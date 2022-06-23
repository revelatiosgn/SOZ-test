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
        EventsBus eventsBus = systems.GetShared<SharedData>().EventsBus;

        if (eventsBus.HasEventSingleton<PlayerMoveEvent>(out PlayerMoveEvent moveEvent))
        {
            foreach (int entity in _filter.Value)
            {
                ref NavAgentData navAgentData = ref _navAgentPool.Value.Get(entity);
                navAgentData.NavMeshAgent.destination = moveEvent.Destination;
                navAgentData.NavMeshAgent.isStopped = false;

                ref CombatData combatData = ref _combatPool.Value.Get(entity);
                combatData.TargetEnitity = -1;
            }
        }
    }
}

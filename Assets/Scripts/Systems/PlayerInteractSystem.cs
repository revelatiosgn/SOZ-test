using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

public class PlayerInteractSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<PlayerTag, CombatData, NavAgentData>> _filter = default;
    private readonly EcsPoolInject<CombatData> _combatPool = default;
    private readonly EcsPoolInject<NavAgentData> _navAgentPool = default;

    public void Run(EcsSystems systems)
    {
        EventsBus eventsBus = systems.GetShared<SharedData>().EventsBus;

        if (eventsBus.HasEventSingleton<PlayerInteractEvent>(out PlayerInteractEvent playerInteractEvent))
        {
            foreach (int entity in _filter.Value)
            {
                ref CombatData combatData = ref _combatPool.Value.Get(entity);
                combatData.TargetEnitity = playerInteractEvent.Entity;
                ref NavAgentData navAgentData = ref _navAgentPool.Value.Get(entity);
                navAgentData.NavMeshAgent.isStopped = true;
            }
        }
    }
}

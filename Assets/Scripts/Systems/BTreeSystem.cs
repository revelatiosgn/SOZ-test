using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

public class BTreeSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<BTreeData>> _filter = default;
    private readonly EcsPoolInject<BTreeData> _pool = default;

    private BTBlackboard _blackboard = new BTBlackboard();

    public void Run(EcsSystems systems)
    {
        EventsBus eventsBus = systems.GetShared<SharedData>().EventsBus;
        eventsBus.DestroyEvents<AttackBeginEvent>();
        
        foreach (var eventEnitiy in eventsBus.GetEventBodies<DeathEvent>(out var eventsPool))
        {
            ref DeathEvent deathEvent = ref eventsPool.Get(eventEnitiy);
            ref BTreeData btreeData = ref _pool.Value.Get(deathEvent.DeadEntity);
            btreeData.BTree = null;
        }

        foreach (int entity in _filter.Value)
        {
            _blackboard.Entity = entity;
            _blackboard.Systems = systems;

            ref BTreeData btreeData = ref _pool.Value.Get(entity);
            btreeData.BTree?.Evaluate(_blackboard);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class BTreeSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<BTreeData>> _filter = default;
    private readonly EcsPoolInject<BTreeData> _pool = default;

    private BTBlackboard _blackboard = new BTBlackboard();

    public void Run(EcsSystems systems)
    {
        foreach (int entity in _filter.Value)
        {
            _blackboard.Entity = entity;
            _blackboard.Systems = systems;

            ref BTreeData btreeData = ref _pool.Value.Get(entity);
            btreeData.BTree.Evaluate(_blackboard);
        }
    }
}

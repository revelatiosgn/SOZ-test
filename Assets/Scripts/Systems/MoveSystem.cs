using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class MoveSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<NavAgentData>> _filter = default;
    private readonly EcsPoolInject<NavAgentData> _pool = default;

    private BTBlackboard _blackboard = new BTBlackboard();

    public void Run(EcsSystems systems)
    {
        foreach (int entity in _filter.Value)
        {
        }
    }
}

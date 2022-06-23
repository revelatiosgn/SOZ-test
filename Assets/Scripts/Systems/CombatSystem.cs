using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Voody.UniLeo.Lite;

public class CombatSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
{
    private readonly EcsFilterInject<Inc<CombatData>> _filter = default;
    private readonly EcsPoolInject<CombatData> _combatPool = default;

    public void Run(EcsSystems systems)
    {
        foreach (int entity in _filter.Value)
        {
            ref CombatData combatData = ref _combatPool.Value.Get(entity);

            if (combatData.AttackDelayTimer > 0f)
                combatData.AttackDelayTimer -= Time.deltaTime;
        }
    }

    public void Init(EcsSystems systems)
    {
        MyEventBus.OnAttackBegin += OnAttackBegin;
        MyEventBus.OnAttackEnd += OnAttackEnd;
    }

    public void Destroy(EcsSystems systems)
    {
        MyEventBus.OnAttackBegin -= OnAttackBegin;
        MyEventBus.OnAttackEnd -= OnAttackEnd;
    }

    private void OnAttackBegin(int entity)
    {
        ref CombatData combatData = ref _combatPool.Value.Get(entity);
        combatData.AttackDelayTimer = combatData.AttackDelay;
    }

    private void OnAttackEnd(int entity)
    {
        ref CombatData combatData = ref _combatPool.Value.Get(entity);
    }
}

using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class CombatSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<CombatData>> _filter = default;
    private readonly EcsPoolInject<CombatData> _combatPool = default;
    private readonly EcsPoolInject<HealthData> _healthPool = default;

    public void Run(EcsSystems systems)
    {
        EventsBus eventsBus = systems.GetShared<SharedData>().EventsBus;
        eventsBus.DestroyEvents<AttackDamageEvent>();

        // Attack delay
        foreach (int entity in _filter.Value)
        {
            ref CombatData combatData = ref _combatPool.Value.Get(entity);

            if (combatData.AttackDelayTimer > 0f)
                combatData.AttackDelayTimer -= Time.deltaTime;

            if (combatData.TargetEnitity != -1)
            {
                if (!_healthPool.Value.Has(combatData.TargetEnitity) || _healthPool.Value.Get(combatData.TargetEnitity).IsDead)
                    combatData.TargetEnitity = -1;
            }
        }

        // Attack begin
        foreach (var eventEnitiy in eventsBus.GetEventBodies<AttackBeginEvent>(out var eventsPool))
        {
            int attackerEntity = eventsPool.Get(eventEnitiy).AttackerEnity;

            ref CombatData selfCombatData = ref _combatPool.Value.Get(attackerEntity);
            selfCombatData.AttackDelayTimer = selfCombatData.AttackDelay;
        }

        // Attack exec
        foreach (var eventEnitiy in eventsBus.GetEventBodies<AttackExecEvent>(out var eventsPool))
        {
            int attackerEntity = eventsPool.Get(eventEnitiy).AttackerEnity;

            ref CombatData selfCombatData = ref _combatPool.Value.Get(attackerEntity);
            ref CombatData targetCombatData = ref _combatPool.Value.Get(selfCombatData.TargetEnitity);
            targetCombatData.TargetEnitity = attackerEntity;

            eventsBus.NewEvent<AttackDamageEvent>() = new AttackDamageEvent { DamagedEntity = selfCombatData.TargetEnitity, Damage = selfCombatData.AttackDamage };
        }
        
        // Death
        foreach (var eventEnitiy in eventsBus.GetEventBodies<DeathEvent>(out var eventsPool))
        {
            int deadEntity = eventsPool.Get(eventEnitiy).DeadEntity;
            ref CombatData combatData = ref _combatPool.Value.Get(deadEntity);
            combatData.TargetEnitity = -1;
        }
    }
}

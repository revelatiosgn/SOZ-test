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
    private readonly EcsPoolInject<PlayerTag> _playerPool = default;

    public void Run(EcsSystems systems)
    {
        EventsBuffer eventsBuffer = systems.GetShared<SharedData>().EventsBuffer;

        foreach (int entity in _filter.Value)
        {
            ref CombatData combatData = ref _combatPool.Value.Get(entity);

            // Attack delay
            if (combatData.AttackDelayTimer > 0f)
                combatData.AttackDelayTimer -= Time.deltaTime;

            // Target dead - reset target
            if (combatData.TargetEnitity != -1)
            {
                if (!_healthPool.Value.Has(combatData.TargetEnitity) || _healthPool.Value.Get(combatData.TargetEnitity).IsDead)
                    combatData.TargetEnitity = -1;
            }
        }

        // Attack begin
        foreach (var eventEnitiy in eventsBuffer.GetEventBodies<AttackBeginEvent>(out var eventsPool))
        {
            int attackerEntity = eventsPool.Get(eventEnitiy).AttackerEnity;

            ref CombatData selfCombatData = ref _combatPool.Value.Get(attackerEntity);
            selfCombatData.AttackDelayTimer = selfCombatData.AttackDelay;
        }

        // Attack exec
        foreach (var eventEnitiy in eventsBuffer.GetEventBodies<AttackExecEvent>(out var eventsPool))
        {
            int attackerEntity = eventsPool.Get(eventEnitiy).AttackerEnity;
            if (attackerEntity == -1)
                continue;

            ref CombatData attackerCombatData = ref _combatPool.Value.Get(attackerEntity);
            if (attackerCombatData.TargetEnitity == -1)
                continue;

            ref CombatData targetCombatData = ref _combatPool.Value.Get(attackerCombatData.TargetEnitity);
            
            eventsBuffer.NewEvent<AttackDamageEvent>() = new AttackDamageEvent { DamagedEntity = attackerCombatData.TargetEnitity, Damage = attackerCombatData.AttackDamage };


            // Player attacked
            if (_playerPool.Value.Has(attackerCombatData.TargetEnitity))
                continue;
            
            // TODO: обрабатывать в vision system
            targetCombatData.TargetEnitity = attackerEntity;
        }
        
        // Death
        foreach (var eventEnitiy in eventsBuffer.GetEventBodies<DeathEvent>(out var eventsPool))
        {
            int deadEntity = eventsPool.Get(eventEnitiy).DeadEntity;
            ref CombatData combatData = ref _combatPool.Value.Get(deadEntity);
            combatData.TargetEnitity = -1;
        }
    }
}

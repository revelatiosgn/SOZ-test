using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class HealthSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<HealthData>> _filter = default;
    private readonly EcsPoolInject<HealthData> _healthPool = default;

    public void Run(EcsSystems systems)
    {
        EventsBuffer eventsBuffer = systems.GetShared<SharedData>().EventsBuffer;
        
        foreach (var eventEnitiy in eventsBuffer.GetEventBodies<AttackDamageEvent>(out var eventsPool))
        {
            ref AttackDamageEvent attackDamageEvent = ref eventsPool.Get(eventEnitiy);

            ref HealthData healthData = ref _healthPool.Value.Get(attackDamageEvent.DamagedEntity);
            healthData.Health -= attackDamageEvent.Damage;

            if (healthData.Health <= 0f)
            {
                eventsBuffer.NewEvent<DeathEvent>().DeadEntity = attackDamageEvent.DamagedEntity;
            }
        }
    }
}

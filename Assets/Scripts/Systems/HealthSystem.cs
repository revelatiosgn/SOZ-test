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
        EventsBus eventsBus = systems.GetShared<SharedData>().EventsBus;
        eventsBus.DestroyEvents<DeathEvent>();
        
        foreach (var eventEnitiy in eventsBus.GetEventBodies<AttackDamageEvent>(out var eventsPool))
        {
            ref AttackDamageEvent attackDamageEvent = ref eventsPool.Get(eventEnitiy);

            ref HealthData healthData = ref _healthPool.Value.Get(attackDamageEvent.DamagedEntity);
            healthData.Health -= attackDamageEvent.Damage;

            if (healthData.Health <= 0f)
            {
                eventsBus.NewEvent<DeathEvent>().DeadEntity = attackDamageEvent.DamagedEntity;
            }
        }
    }
}

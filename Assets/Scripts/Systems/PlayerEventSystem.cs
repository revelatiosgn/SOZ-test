using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class PlayerEventSystem : IEcsRunSystem
{
    private readonly EcsPoolInject<HealthData> _healthPool = default;
    
    public void Run(EcsSystems systems)
    {
        EventsBus eventsBus = systems.GetShared<SharedData>().EventsBus;

        if (eventsBus.HasEventSingleton<PlayerClickEvent>(out PlayerClickEvent clickEvent))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(clickEvent.MousePosition), out RaycastHit charHit, float.MaxValue, LayerMask.GetMask("Character")))
            {
                if (charHit.collider.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    int entity = convertToEntity.TryGetEntity().Value;
                    if (_healthPool.Value.Has(entity) && !_healthPool.Value.Get(entity).IsDead)
                    {
                        eventsBus.NewEventSingleton<PlayerInteractEvent>().Entity = convertToEntity.TryGetEntity().Value;
                        return;
                    }
                }
            }

            if (Physics.Raycast(Camera.main.ScreenPointToRay(clickEvent.MousePosition), out RaycastHit groundHit, float.MaxValue, LayerMask.GetMask("Ground")))
            {
                eventsBus.NewEventSingleton<PlayerMoveEvent>().Destination = groundHit.point;
                return;
            }
        }
    }
}

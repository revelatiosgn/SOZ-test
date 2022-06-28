using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class PlayerEventSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<PlayerTag>> _filter = default;
    private readonly EcsPoolInject<HealthData> _healthPool = default;
    private readonly EcsPoolInject<PlayerTag> _playerPool = default;
    
    public void Run(EcsSystems systems)
    {
        foreach (int entity in _filter.Value)
        {
            ref HealthData healthData = ref _healthPool.Value.Get(entity);
            if (healthData.IsDead)
                return;
        }

        EventsBuffer eventsBuffer = systems.GetShared<SharedData>().EventsBuffer;

        foreach (var eventEnitiy in eventsBuffer.GetEventBodies<PlayerClickEvent>(out var eventsPool))
        {
            Vector2 mousePosition = eventsPool.Get(eventEnitiy).MousePosition;

            RaycastHit[] charHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(mousePosition), float.MaxValue, LayerMask.GetMask("Character"));

            foreach (RaycastHit charHit in charHits)
            {
                if (charHit.collider.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    int entity = convertToEntity.TryGetEntity().Value;
                    if (_playerPool.Value.Has(entity) || !_healthPool.Value.Has(entity) || _healthPool.Value.Get(entity).IsDead)
                        continue;

                    eventsBuffer.NewEvent<PlayerInteractEvent>().Entity = convertToEntity.TryGetEntity().Value;
                    return;
                }
            }

            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out RaycastHit groundHit, float.MaxValue, LayerMask.GetMask("Ground")))
            {
                eventsBuffer.NewEvent<PlayerMoveEvent>().Destination = groundHit.point;
                return;
            }
        }
    }
}

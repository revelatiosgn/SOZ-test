using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class PlayerEventSystem : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        EventsBus eventsBus = systems.GetShared<SharedData>().EventsBus;

        if (eventsBus.HasEventSingleton<PlayerClickEvent>(out PlayerClickEvent clickEvent))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(clickEvent.MousePosition), out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                {
                    eventsBus.NewEventSingleton<PlayerInteractEvent>().Entity = convertToEntity.TryGetEntity().Value;
                }
                else
                {
                    eventsBus.NewEventSingleton<PlayerMoveEvent>().Destination = hit.point;
                }
            }
        }
    }
}

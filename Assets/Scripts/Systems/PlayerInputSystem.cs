using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        if (Input.GetMouseButtonUp(0))
        {
            EventsBuffer eventsBuffer = systems.GetShared<SharedData>().EventsBuffer;
            eventsBuffer.NewEvent<PlayerClickEvent>().MousePosition = Input.mousePosition;
        }
    }
}

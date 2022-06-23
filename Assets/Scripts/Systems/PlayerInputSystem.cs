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
            EventsBus eventsBus = systems.GetShared<SharedData>().EventsBus;
            eventsBus.NewEventSingleton<PlayerClickEvent>().MousePosition = Input.mousePosition;
        }
    }
}

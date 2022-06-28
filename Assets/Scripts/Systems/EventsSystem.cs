using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class EventsSystem : IEcsRunSystem
{
    private List<EventsBus> _eventBuses = new List<EventsBus> { new EventsBus(), new EventsBus() };
    private int _eventBusIndex = 0;

    public void Run(EcsSystems systems)
    {
        EventsBus eventsBus = systems.GetShared<SharedData>().EventsBus;
    }
}

public class EventsBuffer
{
    private EventsBus writeBus = new EventsBus();
    private EventsBus readBus = new EventsBus();

    public void Switch()
    {
        EventsBus eventsBus = writeBus;
        writeBus = readBus;
        readBus = eventsBus;

        writeBus.DestroyAllEvents();
    }
}
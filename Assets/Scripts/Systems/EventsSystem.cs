using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class EventsSystem : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        EventsBuffer eventsBuffer = systems.GetShared<SharedData>().EventsBuffer;
        eventsBuffer.Switch();
    }
}

public class EventsBuffer
{
    private EventsBus writeBus = new EventsBus();
    private EventsBus readBus = new EventsBus();
    
    public ref T NewEvent<T>() where T : struct, IEventReplicant
    {
        return ref writeBus.NewEvent<T>();
    }

    public EcsFilter GetEventBodies<T>(out EcsPool<T> pool) where T : struct, IEventReplicant
    {
        return readBus.GetEventBodies<T>(out pool);
    }

    public void Switch()
    {
        EventsBus eventsBus = writeBus;
        writeBus = readBus;
        readBus = eventsBus;

        writeBus.DestroyAllEvents();
    }

    public void Destroy()
    {
        writeBus.Destroy();
        readBus.Destroy();
    }
}
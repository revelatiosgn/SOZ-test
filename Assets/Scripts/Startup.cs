using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class Startup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;
    private SharedData _sharedData;

    private void Start()
    {
        _world = new EcsWorld();

        _sharedData = new SharedData
        {
            EventsBus = new EventsBus()
        };

        _systems = new EcsSystems(_world, _sharedData);
        _systems
            .ConvertScene()
            .Add(new PlayerInputSystem())
            .Add(new PlayerEventSystem())
            .Add(new PlayerMoveSystem())
            .Add(new PlayerInteractSystem())
            .Add(new BTreeSystem())
            .Add(new VisionSystem())
            .Add(new AnimationSystem())
            .Add(new CombatSystem())
            .Add(_sharedData.EventsBus.GetDestroyEventsSystem()
                .IncSingleton<PlayerClickEvent>()
                .IncSingleton<PlayerMoveEvent>()
                .IncSingleton<PlayerInteractEvent>()
                .IncReplicant<AttackEvent>())
            .Inject()
            .Init();
    }

    private void Update()
    {
        _systems.Run();
    }

    private void OnDestroy()
    {
        _systems.Destroy();
        _world.Destroy();
        _sharedData.EventsBus.Destroy();
    }
}

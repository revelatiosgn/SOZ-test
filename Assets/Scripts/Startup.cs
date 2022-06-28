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
    private static Startup _instance;

    private EcsWorld _world;
    private EcsSystems _systems;
    private SharedData _sharedData;

    public static EcsWorld World => _instance._world;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _world = new EcsWorld();

        _sharedData = new SharedData
        {
            EventsBuffer = new EventsBuffer()
        };

        _systems = new EcsSystems(_world, _sharedData);
        _systems
            .ConvertScene()
            .Add(new PlayerInputSystem())
            .Add(new PlayerEventSystem())
            .Add(new PlayerMoveSystem())
            .Add(new PlayerInteractSystem())
            .Add(new VisionSystem())
            .Add(new AnimationSystem())
            .Add(new CombatSystem())
            .Add(new HealthSystem())
            .Add(new MoveSystem())
            .Add(new BTreeSystem())
            .Add(new EventsSystem())
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
        _sharedData.EventsBuffer.Destroy();
    }
}
